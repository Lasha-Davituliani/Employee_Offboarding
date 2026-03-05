namespace Employee_Offboarding.Application.DTOs.Forms
{
    public sealed record FormPreviewDto(IReadOnlyList<DepartmentPreviewDto> Departments);
    public sealed record DepartmentPreviewDto(int Id, string Name, IReadOnlyList<string> Items);
}
