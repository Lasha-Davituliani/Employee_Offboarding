using Employee_Offboarding.Domain.Entities;

namespace Employee_Offboarding.Application.DTOs.Forms
{
    public sealed record ClearanceFormItemUpsertDto
    (
        int ItemId,
        int DepartmentId,
        ClearanceFormItemStatus Status,
        string? Note,
        int? DepartmentResponsiblePersonId,
        int? ServiceCenterResponsiblePersonId
        );
}
