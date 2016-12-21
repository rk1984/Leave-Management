using System;
using System.Collections.Generic;
using ABC.LeaveManagement.Service.Dto;

namespace ABC.LeaveManagement.Service
{
    public interface IAbsenceRequestService
    {
        void AddAbsenceRequest(AddAbsenceRequestDto addAbsenceRequest);
        void ApproveAbsenceRequest(int id);
        AbsenceRequestDto GetAbsenceRequest(int id);
        ListAbsenceRequestDto ListAbsenceRequest(bool approved);
    }
}
