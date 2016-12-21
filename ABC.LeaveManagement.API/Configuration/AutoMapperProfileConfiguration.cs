using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABC.LeaveManagement.API.ViewModels;
using AutoMapper;
using ABC.LeaveManagement.Service.Dto;
using ABC.LeaveManagement.Core.Entities;
using Microsoft.AspNetCore.Routing.Constraints;

namespace ABC.LeaveManagement.API.Configuration
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<AbsenceRequest, AbsenceRequestDto>();
            CreateMap<AddAbsenceRequestDto, AbsenceRequest>();
            
            CreateMap<AbsenceRequestDto, AbsenceRequestViewModel>()
                .ForMember(dest => dest.RequestStatus, opts => opts.MapFrom(src => src.RequestStatus.ToString()));
            CreateMap<ListAbsenceRequestDto, ListAbsenceRequestViewModel>();
            CreateMap<AddAbsenceRequestDto, AddedAbsenceRequestViewModel>();

            CreateMap<Employee, EmployeeDto>();

            CreateMap<EmployeeDto, EmployeeViewModel>()
                .ForMember(dest => dest.JobPosition, opts => opts.MapFrom(src => src.JobPosition.ToString()));
        }
    }
}
