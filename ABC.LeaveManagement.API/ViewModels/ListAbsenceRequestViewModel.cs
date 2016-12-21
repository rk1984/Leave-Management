using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABC.LeaveManagement.API.ViewModels
{
    public class ListAbsenceRequestViewModel
    {

        public IEnumerable<AbsenceRequestViewModel> ListAbsenceRequests { get; set; }
    }
}
