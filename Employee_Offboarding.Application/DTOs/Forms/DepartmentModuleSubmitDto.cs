namespace Employee_Offboarding.Application.DTOs.Forms
{
    public sealed record DepartmentModuleSubmitDto
    (
        int ClearanceFormId,
        int DepartmentId,
        IEnumerable<ClearanceFormItemUpsertDto> Items,
        string? ManagerComment
        );
}
