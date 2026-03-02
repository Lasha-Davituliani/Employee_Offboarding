namespace Employee_Offboarding.Application.DTOs.Employee
{
    public sealed record EmployeeCreateDto
        (
        string FirstName,
        string LastName,
        string PersonalNumber,
        string? PersonalEmail,
        string? CorporateEmail,
        string PositionSitle,
        int? DepartmentId,
        int? ServiceCenterId,
        int? DirectManagerUserId
        );
}
