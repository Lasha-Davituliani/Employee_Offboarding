namespace Employee_Offboarding.Application.DTOs.Employee
{
    public sealed record EmployeeUpdateDto
    (
        int Id,
        string FirstName,
        string LastName,
        string PersonalNumber,
        string? PersonalEmail,
        string? CorporateEmail,
        string PositionSitle,
        int? DepartmentId,
        int? ServiceCenterId,
        int? DirectManagerUserId,
        bool IsActive
        );
}
