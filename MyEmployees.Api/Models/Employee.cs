namespace MyEmployees.Api.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public int DepartmentId { get; set; } 
        public Department? Department { get; set; }
    }
}
