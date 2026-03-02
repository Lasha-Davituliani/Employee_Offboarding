using Employee_Offboarding.Domain.Entities;

namespace Employee_Offboarding.Application.DTOs.Pdf
{
    public sealed class PdfFromDataDto
    {
        public int Id { get; set; }
        public string EmployeeFullName { get; set; } = string.Empty;
        public string PersonalNumber { get; set; } = string.Empty;
        public string WorkplaceLabel { get; set; } = string.Empty;
        public string PositionTitle { get; set; } = string.Empty;
        public string DirectManagerName { get; set; } = string.Empty;
        public DateTime CreatedAtUtc { get; set; }        
        public DateTime? EmployeeConfirmedAtUtc { get; set; }
        public bool? EmployeeAgreed { get; set; }
        public string? EmployeeComment { get; set; }        
        public string? ForceConfirmedByUsername { get; set; }
        public ModuleDto[] Modules { get; set; } = Array.Empty<ModuleDto>();

        public sealed class ModuleDto
        {
            public string DepartmentName { get; set; } = string.Empty;            
            public string CompletedBy { get; set; } = "N/A";
            public string? ManagerComment { get; set; }           
            public ItemDto[] Items { get; set; } = Array.Empty<ItemDto>();
        }

        public sealed class ItemDto
        {
            public string Name { get; set; } = string.Empty;
            public ClearanceFormItemStatus Status { get; set; }
            public string? TextValue { get; set; }
        }
    }
}