namespace Employee_Offboarding.Application.DTOs.Responsible
{
    public sealed record DepartmentResponsiblePersonUpsertDto
    (
        int? Id,
        int DepartmentId,
        string FullName,
        string Email,
        string? Phone,
        bool IsActive
        );
}
