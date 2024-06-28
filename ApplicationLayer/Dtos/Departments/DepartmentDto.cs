namespace ApplicationLayer.Dtos.Departments
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public List<string> Employees { get; set; } = new List<string>();
    }
}
