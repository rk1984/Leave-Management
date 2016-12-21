using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABC.LeaveManagement.Core.Enum;

namespace ABC.LeaveManagement.Core.Entities
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public JobPosition JobPosition { get; set; }
        public ICollection<AbsenceRequest> AbsenceRequests { get; set; }
    }
}
