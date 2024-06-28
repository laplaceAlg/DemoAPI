namespace ApplicationLayer.Dtos.Employees
{
    public class CreateEmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string? Address { get; set; }
        public List<int>? DepartmentIds { get; set; }
    }
}
