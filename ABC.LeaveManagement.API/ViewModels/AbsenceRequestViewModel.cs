using ABC.LeaveManagement.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABC.LeaveManagement.API.ViewModels
{
    public class AbsenceRequestViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalDays { get { return (int) (EndDate - StartDate).TotalDays; } }
        public string RequestStatus { get; set; }
    }
}
