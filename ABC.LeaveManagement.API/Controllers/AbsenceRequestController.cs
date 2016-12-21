using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ABC.LeaveManagement.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ABC.LeaveManagement.Service;
using ABC.LeaveManagement.Service.Dto;
using AutoMapper;
using ABC.LeaveManagement.Core;
using ABC.LeaveManagement.Core.Enum;

namespace ABC.LeaveManagement.API.Controllers
{
    [Route("/api/absencerequest")]
    [Produces("application/json")]
    public class AbsenceRequestController : Controller
    {
        private readonly IAbsenceRequestService _absenceRequestService;
        private readonly IEmployeeService _empoyeeService;
        private readonly IMapper _mapper;

        public AbsenceRequestController(IAbsenceRequestService absenceRequestService, IEmployeeService empoyeeService, IMapper mapper)
        {
            _absenceRequestService = absenceRequestService;
            _empoyeeService = empoyeeService;
            _mapper = mapper;
        }

        //GET api/absencerequest
        [HttpGet]
        [Route("/api/absencerequest/")]
        [ProducesResponseType(typeof(ListAbsenceRequestViewModel), 200)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 404)]
        public IActionResult GetApproved()
        {
            try
            {
                var absenceRequests = _mapper.Map<ListAbsenceRequestViewModel>(_absenceRequestService.ListAbsenceRequest(true));
                return Ok(absenceRequests);
            }
            catch (Exception)
            {
                return BadRequest("An error occurred while getting list of absene requests");
            }
        }

        //GET api/absencerequest/1
        [HttpGet]
        [Route("/api/absencerequest/{id}")]
        [Produces(typeof(int))]
        [ProducesResponseType(typeof(AbsenceRequestViewModel), 200)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 404)]
        public IActionResult Get(int id)
        {
            try
            {
                var absenceRequests = _mapper.Map<AbsenceRequestViewModel>(_absenceRequestService.GetAbsenceRequest(id));
                return Ok(absenceRequests);
            }
            catch (Exception)
            {
                return BadRequest("An error occurred while getting list of absene requests");
            }
        }

        // POST api/absencerequest
        [HttpPost]
        [Produces(typeof(AddAbsenceRequestDto))]
        [ProducesResponseType(typeof(AddedAbsenceRequestViewModel), 200)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 404)]
        public IActionResult Post([FromBody] AddAbsenceRequestDto addAbsenceRequest)
        {
            if (ModelState.IsValid)
            {
                _absenceRequestService.AddAbsenceRequest(addAbsenceRequest);
                var addAbsenceRequestViewModel = _mapper.Map<AddedAbsenceRequestViewModel>(addAbsenceRequest);
                return Ok(addAbsenceRequestViewModel);
            }

            return BadRequest("Failed to save the request");
        }

        // POST api/values
        [HttpPost]
        [Route("/api/absencerequest/approve")]
        [Produces(typeof(ApproveAbsenceRequestViewModel))]
        [ProducesResponseType(typeof(OkObjectResult), 200)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 404)]
        [ProducesResponseType(typeof(UnauthorizedResult), 401)]
        public IActionResult Post([FromBody]ApproveAbsenceRequestViewModel approveAbsenceRequestViewModel)
        {
            if (ModelState.IsValid)
            {
                var emplyee = _empoyeeService.GetEmployee(approveAbsenceRequestViewModel.EmployeeId);

                if (emplyee == null)
                    return BadRequest("Employee doesn't exist");

                if(emplyee.JobPosition != JobPosition.Admin)
                    return Unauthorized();                

                _absenceRequestService.ApproveAbsenceRequest(approveAbsenceRequestViewModel.AbsenceRequestId);
                return Ok("Absense request approved");
            }

            return BadRequest("Failed to approve the request");
        }
    }
}
