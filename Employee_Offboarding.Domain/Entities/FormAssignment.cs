namespace Employee_Offboarding.Domain.Entities
{
    public enum ClearanceSubStatus { Pending = 0, InReview = 1, Approved = 2 }
    public class FormAssignment
    {
        public int Id { get; set; }
        public int ClearanceFormId { get; set; }
        public ClearenceForm ClearanceForm { get; set; } = default!;
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = default!;
        public int? DepartmentResponsiblePersonId { get; set; }
        public DepartmentResponsiblePerson? DepartmentResponsiblePerson { get; set; }
        public int? ServiceCenterResponsiblePersonId { get; set; }
        public ServiceCenterResponsiblePerson? ServiceCenterResponsiblePerson { get; set; }
        public string Email { get; set; } = default!;
        public Guid Token { get; set; }
        public ClearanceSubStatus SubStatus { get; set; } = ClearanceSubStatus.Pending;
        public bool RemainderSent { get; set; } = false;
        public DateTime? LastRemainderSentAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? NotifiedAt { get; set; }
        public string? ManageComment { get; set; }

    }
}
