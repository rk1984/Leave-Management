using ABC.LeaveManagement.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ABC.LeaveManagement.API.ViewModels
{
    public class AddedAbsenceRequestViewModel
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDateTime { get { return DateTime.Now; } }
    }
}
