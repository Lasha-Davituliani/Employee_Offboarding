namespace Employee_Offboarding.Domain.Entities
{
    public enum ItemKind { Checkbox = 1, Text = 2 }
    public enum ItemStatusType
    {
        SimpleReturned = 1,
        WithConditionalComment = 2,
        PowerOfAttorney = 3,
        Debt = 4,
        YesNo = 5,
        TypeForSales = 6,
        SimpleReturned2 = 7,
        TypeForSales2 = 8,
        CashierResponsibility = 9
    }
    public class Item
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = default!;
        public string Name { get; set; } = default!;
        public ItemKind Kind { get; set; } = ItemKind.Checkbox;
        public bool IsActive { get; set; } = true;
        public int DisplayOrder { get; set; } = 0;
        public ItemStatusType StatusType { get; set; } = ItemStatusType.SimpleReturned;
        public ICollection<ClearenceFormItem> ClearanceFormItems { get; set; } = new List<ClearenceFormItem>();

    }
}
