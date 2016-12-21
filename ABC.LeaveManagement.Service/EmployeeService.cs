using ABC.LeaveManagement.Core.Data;
using ABC.LeaveManagement.Core.Entities;
using ABC.LeaveManagement.Service.Dto;
using AutoMapper;

namespace ABC.LeaveManagement.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IRepository<Employee> employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public EmployeeDto GetEmployee(int id)
        {
            var employee = _mapper.Map<EmployeeDto>(_employeeRepository.Get(id));
            return employee;
        }
    }
}