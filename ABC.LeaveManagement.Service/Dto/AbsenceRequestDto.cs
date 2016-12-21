using System;
using ABC.LeaveManagement.Core.Enum;

namespace ABC.LeaveManagement.Service.Dto
{
    public class AbsenceRequestDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public RequestStatus RequestStatus { get; set; }
    }
}