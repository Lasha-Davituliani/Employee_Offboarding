namespace Employee_Offboarding.Application.DTOs.Forms
{
    public sealed record ClearanceFormCreateDto
    (
        int EmployeeId,
        int InitiatedByUserId,
        int? DepartmentId,
        int? ServiceCenterId
        );
}
