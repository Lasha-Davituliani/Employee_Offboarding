namespace Employee_Offboarding.Application.DTOs.Forms
{
    public sealed record EmployeeConfirmationDto
    (
        int ClearanceFormId,
        bool IsAgreed,
        string? Comment
        );
}
