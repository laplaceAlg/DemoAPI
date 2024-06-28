namespace ApplicationLayer.Dtos.Employees
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public List<string> Departments { get; set; } = new List<string>();
    }
}
