using System;
using ABC.LeaveManagement.Core.Enum;

namespace ABC.LeaveManagement.Core.Entities
{
    public class AbsenceRequest : BaseEntity
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public RequestStatus RequestStatus { get; set; }

        public virtual int EmployeeId { get; set; }
    }
}