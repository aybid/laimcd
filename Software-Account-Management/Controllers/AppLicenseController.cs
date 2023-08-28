using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Software_Account_Management.Data;
using Software_Account_Management.Models;
using static Software_Account_Management.Models.LicenseOrderBookResponse;

namespace Software_Account_Management.Controllers;

public static class AppLicenseController
{
    public static void MapAppLicenseEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/AppLicense").WithTags(nameof(AppLicense));

        group.MapGet("/", async (int spaceid, Software_Account_ManagementContext db) =>
        {
            return await db.AppLicenses
            .Include(model => model.Reservation)
            .Where(model => model.SpaceId == spaceid && model.LicenseStatus)
            .Select(appLicense => (AppLicenseResponse)appLicense)
            .ToListAsync();    
        })
        .WithName("GetAllAppLicenses")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<AppLicenseResponse>, NotFound>> (Guid id, Software_Account_ManagementContext db) =>
        {
            return await db.AppLicenses.AsNoTracking()
                .Include(model => model.Reservation)
                .Select(appLicense => (AppLicenseResponse)appLicense)
                .FirstOrDefaultAsync(model => model.Id == id)
                is AppLicenseResponse model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetAppLicenseById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (Guid id, AppLicenseRequest appLicense, Software_Account_ManagementContext db) =>
        {
            var affected = await db.AppLicenses
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.AppName, appLicense.AppName)
                  .SetProperty(m => m.AppService, appLicense.AppService)
                  .SetProperty(m => m.SpaceId, appLicense.SpaceId)
                  .SetProperty(m => m.TestStationPool, appLicense.TestStationPool)
                  .SetProperty(m => m.UserName, appLicense.UserName)
                  .SetProperty(m => m.Password, appLicense.Password)
                  .SetProperty(m => m.LastModified, DateTime.UtcNow)
                  .SetProperty(m => m.LicenseStatus, appLicense.LicenseStatus)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateAppLicense")
        .WithOpenApi();

        group.MapPost("/", async (AppLicenseRequest applicensereq, Software_Account_ManagementContext db) =>
        {
            var license_entry = (AppLicense)applicensereq;
            db.AppLicenses.Add(license_entry);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/AppLicense/{license_entry.Id}",(AppLicenseResponse)license_entry);
        })
        .WithName("CreateAppLicense")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (Guid id, Software_Account_ManagementContext db) =>
        {
            var affected = await db.AppLicenses
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(m => m.LicenseStatus, false)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteAppLicense")
        .WithOpenApi();
    }
}
