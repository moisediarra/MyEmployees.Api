namespace MyEmployees.Api.DTOs
{
    public class CreateEmployeeDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
    }
}
