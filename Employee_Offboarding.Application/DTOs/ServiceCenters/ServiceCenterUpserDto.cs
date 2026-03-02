namespace Employee_Offboarding.Application.DTOs.ServiceCenters
{
    public sealed record ServiceCenterUpserDto
    (
        int? Id,
        string Name,
        string? Region,
        bool IsActive
        );
}
