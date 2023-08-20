using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Software_Account_Management.Models;

namespace Software_Account_Management.Data
{
    public class Software_Account_ManagementContext : DbContext
    {
        public Software_Account_ManagementContext (DbContextOptions<Software_Account_ManagementContext> options)
            : base(options)
        {
        }

        public DbSet<AppLicense> AppLicenses { get; set; } = default!;
        public DbSet<LicenseOrderBook> LicenseOrderBooks { get; set; } = default!;
        public DbSet<LicenseQueue> licenseQueues { get; set; } = default!;

    }
}
