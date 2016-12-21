using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABC.LeaveManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ABC.LeaveManagement.Data.Configuration
{
    public class EmployeeConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(p =>
            {
                p.HasKey(x => x.Id);
                p.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                p.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(50);

                p.Property(x => x.LastName)
                   .IsRequired()
                   .HasMaxLength(50);
            });
        }
    }
}
