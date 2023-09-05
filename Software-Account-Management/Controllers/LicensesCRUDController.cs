using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Software_Account_Management.Data;
using Software_Account_Management.Models;
using Software_Account_Management.Services;

namespace Software_Account_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicensesCRUDController : ControllerBase
    {
        private readonly SoftwareLicenseServiceContext _context;
        private readonly CacheService _cacheService;
        public LicensesCRUDController(SoftwareLicenseServiceContext context, CacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        // GET: api/Licenses_new_/{id}
        [HttpGet("{licenseid}", Name = "GetLicenseById")]
        public async Task<ActionResult<LicenseResponse>> GetLicense(Guid licenseid)
        {

          var license_resp = await _context.Licenses.AsNoTracking()
                            .Include(model => model.Application)
                            .Include(model => model.LicenseVendor)
                            .FirstOrDefaultAsync(model => model.LicenseId == licenseid);

            if (license_resp == null)
            {
                return NotFound();
            }

            return Ok((LicenseResponse)license_resp);
        }


        [HttpPut("{licenseid}")]
        public async Task<IActionResult> PutLicense(Guid licenseid, LicenseRequest licensereq)
        {
            // Check if license exists
            var license_exists = await _context.Licenses.AsNoTracking()
                .FirstOrDefaultAsync(model => model.LicenseId == licenseid);

            if (license_exists == null)
            {
                return NotFound(licenseid);
            }

            // get the id for application
            var application = await _context.Applications.AsNoTracking()
                .FirstOrDefaultAsync(model => model.ApplicationName == licensereq.ApplicationName
                && model.ApplicationVersion == licensereq.ApplicationVersion);

            if (application == null)
            {
                application = new Application
                {
                    ApplicationId = Guid.NewGuid(),
                    ApplicationName = licensereq.ApplicationName,
                    ApplicationVersion = licensereq.ApplicationVersion
                };

                _context.Applications.Add(application);
                await _context.SaveChangesAsync();
            }

            // get id for vendor
            var vendor = await _context.LicenseVendors.AsNoTracking()
                .FirstOrDefaultAsync(app => app.VendorName == licensereq.LicenseVendor);

            if (vendor == null)
            {
                vendor = new LicenseVendor { LicenseVendorId = Guid.NewGuid(), VendorName = licensereq.LicenseVendor };

                _context.LicenseVendors.Add(vendor);
                await _context.SaveChangesAsync();
            }

            var affected = await _context.Licenses
                .Where(model => model.LicenseId == licenseid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.ApplicationId, application.ApplicationId)
                  .SetProperty(m => m.LicenseVendorId, vendor.LicenseVendorId)
                  .SetProperty(m => m.SpaceId, licensereq.SpaceName.Length) // need DAL api to fix this
                  .SetProperty(m => m.TestStationPool, licensereq.TestStationPool.Count > 0 ? string.Join(":", licensereq.TestStationPool) : null)
                  .SetProperty(m => m.UserName, licensereq.UserName)
                  .SetProperty(m => m.Password, licensereq.Password)
                  .SetProperty(m => m.LastModified, DateTime.UtcNow)
                );

            return affected == 1 ? Ok() : NotFound();
        }

        // POST: api/Licenses_new_
        [HttpPost]
        public async Task<ActionResult<LicenseResponse>> CreateLicense(LicenseRequest licensereq)
        {

            // First get the id for application name
            var application = await _context.Applications.AsNoTracking()
                .FirstOrDefaultAsync(app => app.ApplicationName == licensereq.ApplicationName 
                && app.ApplicationVersion == licensereq.ApplicationVersion);
            
            if (application == null) 
            {
                application = new Application
                {
                    ApplicationId = Guid.NewGuid(),
                    ApplicationName = licensereq.ApplicationName,
                    ApplicationVersion = licensereq.ApplicationVersion
                };

                _context.Applications.Add(application);
                await _context.SaveChangesAsync();  
            }
            // get id for vendor
            var vendor = await _context.LicenseVendors.AsNoTracking()
                .FirstOrDefaultAsync(app => app.VendorName == licensereq.LicenseVendor);

            if (vendor == null)
            {
                vendor = new LicenseVendor{ LicenseVendorId = Guid.NewGuid(), VendorName = licensereq.LicenseVendor };

                _context.LicenseVendors.Add(vendor);
                await _context.SaveChangesAsync();
            }
            // make the license obj add it.
            var new_license = new License
            {
                LicenseId = Guid.NewGuid(),
                ApplicationId = application.ApplicationId,
                LicenseVendorId = vendor.LicenseVendorId,
                SpaceId = licensereq.SpaceName.Length, //TODO: need to add dal api for this
                TestStationPool = licensereq.TestStationPool.Count>0? string.Join(":", licensereq.TestStationPool): null,
                UserName = licensereq.UserName,
                Password = licensereq.Password,
                CreatedBy = licensereq.CreatedBy,
                LastModified = DateTime.UtcNow,
                ExpiredOn = licensereq.ExpiredOn            
            };
            _context.Licenses.Add(new_license);
            var affected = await _context.SaveChangesAsync();


            return affected == 1 ? Ok((LicenseResponse)new_license) : NotFound();
        }

        //// DELETE: api/Licenses_new_/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLicense(Guid licenseid)
        {

            var affected = await _context.Licenses
                .Where(model => model.LicenseId == licenseid)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(m => m.IsDeleted, true)
                );

            return affected == 1 ? Ok() : NotFound();


        }


    }
}
