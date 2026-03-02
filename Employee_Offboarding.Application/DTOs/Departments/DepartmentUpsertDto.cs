namespace Employee_Offboarding.Application.DTOs.Departments
{
    public sealed record DepartmentUpsertDto(int? Id, string Name, bool IsActive);

}
