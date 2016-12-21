using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABC.LeaveManagement.Service.Dto;

namespace ABC.LeaveManagement.Service
{
    public interface IEmployeeService
    {
        EmployeeDto GetEmployee(int id);
    }
}
