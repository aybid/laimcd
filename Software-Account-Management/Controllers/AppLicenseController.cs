using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Software_Account_Management.Data;
using Software_Account_Management.Models;

namespace Software_Account_Management.Controllers;

public static class AppLicenseController
{
    public static void MapAppLicenseEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/AppLicense").WithTags(nameof(AppLicense));

        group.MapGet("/", async (int spaceid, Software_Account_ManagementContext db) =>
        {
            return await db.AppLicenses.Where(model => model.SpaceId == spaceid).ToListAsync();
        })
        .WithName("GetAllAppLicenses")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<AppLicense>, NotFound>> (string id, Software_Account_ManagementContext db) =>
        {
            return await db.AppLicenses.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == new Guid(id))
                is AppLicense model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetAppLicenseById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (string id, AppLicenseDTO appLicensedto, Software_Account_ManagementContext db) =>
        {
            var affected = await db.AppLicenses
                .Where(model => model.Id == new Guid(id))
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.AppName, appLicensedto.AppName)
                  .SetProperty(m => m.AppService, appLicensedto.AppService)
                  .SetProperty(m => m.SpaceId, appLicensedto.SpaceId)
                  .SetProperty(m => m.TestStationPool, appLicensedto.TestStationPool)
                  .SetProperty(m => m.UserName, appLicensedto.UserName)
                  .SetProperty(m => m.Password, appLicensedto.Password)
                  .SetProperty(m => m.LastModified, DateTime.Now)
                  .SetProperty(m => m.LicenseStatus, appLicensedto.LicenseStatus)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateAppLicense")
        .WithOpenApi();

        group.MapPost("/", async (AppLicenseDTO appLicensedto, Software_Account_ManagementContext db) =>
        {
            var appLicense = new AppLicense
            {
                AppName = appLicensedto.AppName,
                AppService = appLicensedto.AppService,
                SpaceId = appLicensedto.SpaceId,
                TestStationPool = appLicensedto.TestStationPool,
                UserName = appLicensedto.UserName,
                Password = appLicensedto.Password,
                LastModified = DateTime.Now,
                LicenseStatus = appLicensedto.LicenseStatus
            };
            db.AppLicenses.Add(appLicense);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/AppLicense/{appLicense.Id}", appLicense);
        })
        .WithName("CreateAppLicense")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (string id, Software_Account_ManagementContext db) =>
        {
            var affected = await db.AppLicenses
                .Where(model => model.Id == new Guid(id))
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteAppLicense")
        .WithOpenApi();
    }
}
