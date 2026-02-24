namespace Employee_Offboarding.Domain.Entities
{
    public class DepartmentResponsiblePerson
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string? Position { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool IsDefault { get; set; } = false;
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
