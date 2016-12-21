using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABC.LeaveManagement.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ABC.LeaveManagement.Data.IdentityModel
{
    public class LeaveManagementUser : IdentityUser
    {
        public Employee Employee { get; set; }
    }
}
