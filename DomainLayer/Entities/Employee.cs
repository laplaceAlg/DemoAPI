namespace DomainLayer.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public ICollection<Dept_Emp> Dept_Emps { get; set; }
    }
}
