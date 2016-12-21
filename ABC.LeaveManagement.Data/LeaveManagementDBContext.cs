using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABC.LeaveManagement.Core.Data;
using ABC.LeaveManagement.Core.Entities;
using ABC.LeaveManagement.Data.Configuration;
using ABC.LeaveManagement.Data.IdentityModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ABC.LeaveManagement.Data
{
    public class LeaveManagementDbContext : IdentityDbContext<LeaveManagementUser>
    {
        private readonly IConfigurationRoot _config;

        public LeaveManagementDbContext(IConfigurationRoot config, DbContextOptions<LeaveManagementDbContext> options) : base(options)
        {
            _config = config;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            AbsenseRequestConfiguration.Configure(builder);
            EmployeeConfiguration.Configure(builder);
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["ConnectionStrings:LeaveManagementDbContextConnection"]);
        }
    }
}
