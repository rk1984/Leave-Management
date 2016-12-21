using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABC.LeaveManagement.Core.Entities;
using ABC.LeaveManagement.Core.Enum;
using ABC.LeaveManagement.Data.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ABC.LeaveManagement.Data
{
    public class LeaveManagementDbContextSeed
    {
        private readonly LeaveManagementDbContext _context;
        private readonly UserManager<LeaveManagementUser> _userManager;

        public LeaveManagementDbContextSeed(LeaveManagementDbContext context, UserManager<LeaveManagementUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task SeedData()
        {
            if (!_context.Roles.Any())
            {
                var roles = new List<IdentityRole>() {
                    new IdentityRole("Admin"),
                    new IdentityRole("Employee")
                };

                _context.Roles.AddRange(roles);
            }

            if (!_userManager.Users.Any())
            {
                var users = new List<LeaveManagementUser>()
                {
                    new LeaveManagementUser()
                        {
                            Employee =  new Employee() {
                            Name = "Admin",
                            LastName = "User",
                            JobPosition = JobPosition.Admin
                        },
                        Email = "admin@abc.com",
                        UserName = "admin@abc.com"
                    },
                    new LeaveManagementUser()
                    {
                        Employee = new Employee() {
                            Name = "Jhon",
                            LastName = "Smith",
                            JobPosition = JobPosition.Employee,
                            AbsenceRequests = new List<AbsenceRequest>()
                            {
                                new AbsenceRequest() {
                                    Title = "Sick leave",
                                    StartDate = new DateTime(2016, 12, 1),
                                    EndDate = new DateTime(2016, 12, 3),
                                    RequestStatus = RequestStatus.Approved
                                },
                                new AbsenceRequest() {
                                    Title = "End of year holidays",
                                    StartDate = new DateTime(2016, 12, 20),
                                    EndDate = new DateTime(2017, 1, 3),
                                    RequestStatus = RequestStatus.InProgress
                                }
                            }
                        },
                        UserName ="employee@abc.com",
                        Email = "employee@abc.com"
                    }
                };

                foreach (var leaveManagementUser in users)
                    await _userManager.CreateAsync(leaveManagementUser, "P@ssw0rd");

                //await _context.SaveChangesAsync();
            }
        }
    }
}
