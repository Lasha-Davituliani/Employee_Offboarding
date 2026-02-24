using System.ComponentModel.DataAnnotations;

namespace Employee_Offboarding.Domain.Entities
{
    public enum ClearanceFormItemStatus
    {
        [Display(Name = "დასადასტურებელია")]
        Pending = 1,
        [Display(Name = "განხილვაშია")]
        InReview = 2,
        [Display(Name = "დადასტურებულია")]
        Confirmed = 3,
        [Display(Name = "ჩაბარებულია")]
        Returned = 4,
        [Display(Name = "არ ჩაბარებულა")]
        NotReturned = 5,
        [Display(Name = "არ უსარგებლია")]
        NotHad = 6,
        [Display(Name = "არ ფიქსირდება დავალიანება")]
        NoDebt = 7,
        [Display(Name = "ფიქსირედბე დავალიანება")]
        HasDebt = 8,
        [Display(Name = "გაუქმებულია")]
        Cancelled = 9,
        [Display(Name = "არ არის გაუქმებული")]
        NotCancelled = 10,
        [Display(Name = "არ ქონდა(მინდობილობა)")]
        DidNotHave = 11,
        [Display(Name = "კი")]
        Yes = 12,
        [Display(Name = "არა")]
        No = 13,
        [Display(Name = "არ სარგებლობს")]
        NotApplicable = 14,
        [Display(Name = "მიმდინარე")]
        InProgress = 15,
        [Display(Name = "ვადაგადაცილებული სესხი")]
        OverdueLoan = 16,
        [Display(Name = "არ ვრცელდება სალაროს პასუხისმგებლობა")]
        NotApplicableToCashier = 17
    }
    
        
    
    public class ClearenceFormItem
    {
        public int Id { get; set; }
        public int ClearanceFormId { get; set; }
        public ClearenceForm ClearanceForm { get; set; } = default!;
        public int ItemId { get; set; }
        public Item Item { get; set; } = default!;
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = default!;
        public int? DepartmentResponsiblePersonId { get; set; }
        public DepartmentResponsiblePerson? DepartmentResponsiblePerson { get; set; }
        public int? ServiceCenterResponsiblePersonId { get; set; }
        public ServiceCenterResponsiblePerson? ServiceCenterResponsiblePerson { get; set; }
        public ClearanceFormItemStatus Status { get; set; } = ClearanceFormItemStatus.Pending;
        public string? TextValue { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

    }
}
