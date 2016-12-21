using System;
using System.Collections.Generic;
using ABC.LeaveManagement.Core.Enum;

namespace ABC.LeaveManagement.Service.Dto
{
    public class ListAbsenceRequestDto
    {
        public IEnumerable<AbsenceRequestDto> ListAbsenceRequests { get; set; }
    }
}