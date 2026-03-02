namespace Employee_Offboarding.Application.DTOs.Responsible
{
    public sealed record ServiceCenterResponsiblePersonUpsertDto
    (
        int? Id,
        int ServiceCenterId,
        string FullName,
        string Email,
        string? Phone,
        bool IsActive
        );
}
