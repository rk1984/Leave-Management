using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ABC.LeaveManagement.Data;
using ABC.LeaveManagement.Core.Enum;

namespace ABC.LeaveManagement.Data.Migrations
{
    [DbContext(typeof(LeaveManagementDbContext))]
    [Migration("20161221163527_AbsenceRequest_Added_EmployeeId")]
    partial class AbsenceRequest_Added_EmployeeId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ABC.LeaveManagement.Core.Entities.AbsenceRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EmployeeId");

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("RequestStatus");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("AbsenceRequest");
                });

            modelBuilder.Entity("ABC.LeaveManagement.Core.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("JobPosition");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("ABC.LeaveManagement.Core.Entities.AbsenceRequest", b =>
                {
                    b.HasOne("ABC.LeaveManagement.Core.Entities.Employee")
                        .WithMany("AbsenceRequests")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
