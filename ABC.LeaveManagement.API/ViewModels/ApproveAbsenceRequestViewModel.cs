using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABC.LeaveManagement.API.ViewModels
{
    public class ApproveAbsenceRequestViewModel
    {
        public int AbsenceRequestId { get; set; }
        public int EmployeeId { get; set; }
    }
}
