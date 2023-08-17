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

        public DbSet<AppLicense> AppLicense { get; set; } = default!;
        public DbSet<LicenseOrderBook> LicenseOrderBook { get; set; } = default!;
        public DbSet<LicenseQueue> licenseQueue { get; set; } = default!;

    }
}
