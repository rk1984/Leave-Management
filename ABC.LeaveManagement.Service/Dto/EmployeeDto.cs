using ABC.LeaveManagement.Core.Enum;

namespace ABC.LeaveManagement.Service.Dto
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public JobPosition JobPosition { get; set; }
    }
}