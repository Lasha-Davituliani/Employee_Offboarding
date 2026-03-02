namespace Employee_Offboarding.Application.DTOs
{
    public record ResponsiblePersonDto(int Id, string FullName, string Email, bool IsActive);

    public record CreateDepartmentResponsibleRequest(int DepartmentId, string FullName, string Email);
    public record UpdateDepartmentResponsibleRequest(string FullName, string Email, bool? Deactivate);

    public record CreateServiceCenterResponsibleRequest(int ServiceCenterId, string FullName, string Email);
    public record UpdateServiceCenterResponsibleRequest(string FullName, string Email, bool? Deactivate);
}
