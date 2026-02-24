using System.ComponentModel.DataAnnotations;

namespace Employee_Offboarding.Domain.Entities
{
    public enum ClarenceStatus
    {
        [Display(Name ="პროცესშია")]
        Pending =1,
        [Display(Name ="თანამშრომლის დასტური")]
        InReview =2,
        [Display(Name ="დადასტურებული")]
        Confirmed = 3,
        [Display(Name ="უარყოფილია")]
        Disputed = 4,
        [Display(Name ="დასრულებული")]
        Completed = 5
    }

    public class ClearenceForm
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = default!;
        public int InitiatedByUserId { get; set; }
        public User InitiatedByUser { get; set; } = default!;
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public int? ServiceCenterId { get; set; }
        public ServiceCenter? ServiceCenter { get; set; }
        public ClarenceStatus Status { get; set; } = ClarenceStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ConfirmedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? EmployeeConfirmedAt { get; set; }
        public DateTime? HrFinalizedAt { get; set; }
        public Guid? EmployeeConfirmationToken { get; set; }
        public bool? EmployeeAgreed { get; set; }
        public string? EmployeeComment { get; set; }
        public int? ForceConfirmedByUserId { get; set; }
        public User? ForceConfirmedByUser { get; set; }
        public ICollection<ClearenceFormDepartment> clearenceFormDepartments { get; set; } = new List<ClearenceFormDepartment>();
        public ICollection<ClearenceFormItem> Items { get; set; } = new List<ClearenceFormItem>();
        public ICollection<FormAssignment> Assigments { get; set; } = new List<FormAssignment>();


    }
}
