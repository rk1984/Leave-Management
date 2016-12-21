using System;
using System.Collections.Generic;
using ABC.LeaveManagement.Core.Data;
using ABC.LeaveManagement.Core.Entities;
using ABC.LeaveManagement.Core.Enum;
using ABC.LeaveManagement.Service.Dto;
using AutoMapper;

namespace ABC.LeaveManagement.Service
{
    public class AbsenceRequestService : IAbsenceRequestService
    {
        private readonly IRepository<AbsenceRequest> _absenceRequestRepository;
        private readonly IMapper _mapper;

        public AbsenceRequestService(IRepository<AbsenceRequest> absenceRequestRepository, IMapper mapper)
        {
            _absenceRequestRepository = absenceRequestRepository;
            _mapper = mapper;
        }

        public void AddAbsenceRequest(AddAbsenceRequestDto addAbsenceRequest)
        {
            if (addAbsenceRequest == null)
                throw new ArgumentNullException();

            var absenceRequest = _mapper.Map<AbsenceRequest>(addAbsenceRequest);
            _absenceRequestRepository.Insert(absenceRequest);
            _absenceRequestRepository.Commit();
        }

        public void ApproveAbsenceRequest(int id)
        {
            var absenceRequest = _absenceRequestRepository.Get(id);
            absenceRequest.RequestStatus = RequestStatus.Approved;
            _absenceRequestRepository.Update(absenceRequest);
        }

        public AbsenceRequestDto GetAbsenceRequest(int id)
        {
            var abseneceRequest = _mapper.Map<AbsenceRequestDto>(_absenceRequestRepository.Get(id));
            return abseneceRequest;
        }

        public ListAbsenceRequestDto ListAbsenceRequest(bool approved)
        {
            var absenceRequestsList = _absenceRequestRepository.GetAll();
            return new ListAbsenceRequestDto() { ListAbsenceRequests = _mapper.Map<IEnumerable<AbsenceRequestDto>>(absenceRequestsList) };
        }
    }
}