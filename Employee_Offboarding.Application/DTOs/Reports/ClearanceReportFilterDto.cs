using Employee_Offboarding.Domain.Entities;

namespace Employee_Offboarding.Application.DTOs.Reports
{
    public sealed record ClearanceReportFilterDto
    (
        DateTime? From,
        DateTime? To,
        int? DepartmentId,
        int? ServiceCenterId,
        ClarenceStatus? Status
        );
}
