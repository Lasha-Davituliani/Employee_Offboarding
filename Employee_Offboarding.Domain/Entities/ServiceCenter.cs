namespace Employee_Offboarding.Domain.Entities
{
    public class ServiceCenter
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public bool IsActive { get; set; } = true;
    }
}
