namespace DomainLayer.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Dept_Emp> Dept_Emps { get; set; }
    }
}
