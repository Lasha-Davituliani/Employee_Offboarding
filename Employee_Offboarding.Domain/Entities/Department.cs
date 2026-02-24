namespace Employee_Offboarding.Domain.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; } = true;
        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
