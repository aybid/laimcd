using Microsoft.EntityFrameworkCore;
using Software_Account_Management.Data;
namespace Software_Account_Management.Controllers;

public static class DropDownController
{
    public static void MapApplicationEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/GetDetails").WithTags("GetDetails");

        group.MapGet("/Applications", async (SoftwareLicenseServiceContext db) =>
        {
            return await db.Applications.Select(model => new { model.ApplicationName, model.ApplicationVersion }).ToListAsync();
        })
       .WithName("GetAllApplications")
       .WithOpenApi();


        group.MapGet("/LicenseVendors", async (SoftwareLicenseServiceContext db) =>
        {
            return await db.LicenseVendors.Select(model => model.VendorName).ToListAsync();
        })
       .WithName("GetAllLicenseVendors")
       .WithOpenApi();

       // group.MapGet("/StructuredLicenseDetails", async Task<Results<Ok<List<StructuredResponse>>, NotFound>> (SoftwareLicenseServiceContext db) =>
       // {
       //     // get the list of all license
       //     // reserved get the ids from 
       //     // unreserved
       //     // 
       //     return TypedResults.NotFound();
       // })
       //.WithName("GetAllLicenseVendors")
       //.WithOpenApi();

    }
}