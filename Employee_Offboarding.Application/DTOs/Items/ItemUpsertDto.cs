namespace Employee_Offboarding.Application.DTOs.Items
{
    public sealed record ItemUpsertDto
    (
    int? Id,
    int DepartmentId,
    string Name,
    bool IsTextRequired,
    bool IsActive
        );
}
