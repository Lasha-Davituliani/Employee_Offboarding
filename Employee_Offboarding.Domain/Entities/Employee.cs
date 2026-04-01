namespace Employee_Offboarding.Domain.Entities
{
    public enum EmployeeStatus { Active = 1, Left = 2 }
    public class Employee
    {
        public int Id { get; set; }
        public string  FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PersonalNumber { get; set; } = default!;
        public EmployeeStatus Status { get; set; } = EmployeeStatus.Active;
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public int? ServiceCenterId { get; set; }
        public ServiceCenter? ServiceCenter { get; set; }
        public ICollection<ClearenceForm> ClearenceForms { get; set; } = new List<ClearenceForm>();
        public string? PositionTitle { get; set; }
        public string? DirectManager { get; set; }
        public string? DirectManagerEmail { get; set; }
    }
}
