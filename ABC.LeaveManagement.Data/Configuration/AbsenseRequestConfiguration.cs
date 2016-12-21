using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABC.LeaveManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ABC.LeaveManagement.Data.Configuration
{
    public class AbsenseRequestConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AbsenceRequest>(p =>
            {
                p.HasKey(x => x.Id);
                p.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                p.Property(x => x.Title)
                   .IsRequired()
                   .HasMaxLength(50);
            });
        }
    }
}
