namespace Employee_Offboarding.Application.Abstractions.Services
{
    public sealed record RegistrationNotifyItem(
        int DepartmentId,
        string DepartmentName,
        string ToEmail,
        string EmployeePersonalNumber,
        string ToDisplayName,
        string Link);
    public interface INotificationService
    {

        Task NotifyManagersOnRegistrationAsync(
            int formId,
            string employeeFullName,
            string employeePersonalNumber,
            string positonTitle,
            string structuralUnit,
            IReadOnlyCollection<RegistrationNotifyItem> recipients,
            CancellationToken ct = default
            );

        Task NotifyManagersOnRegistrationAsync(
            string employeeEmail,
            string employeeFullName,
            Guid confirmationToken,
            CancellationToken ct = default
            );
        Task ResendModuleLinkAsync(
            string toEmail,
            string toDisplayName,
            string EmployeeFullName,
            string departmentName,
            string positionTitle,
            string structuralUnit,
            string link,
            CancellationToken ct = default
            );

        Task NotifyHrForFinalizationAsync(
            string hrEmail,
            string employeeFullName,
            int formId,
            CancellationToken ct = default
            );

        Task NotifyEmployeeOfUpdateAsync(
            string employeeEmail,
            string employeeFullName,
            Guid newConfirmationToken,
            CancellationToken ct = default
            );

        Task SendReminderEmailAsync(
            string toEmail,
            string toDisplayName,
            string employeeFullName,
            string employeePersonalNumber, 
            string departmentName,
            string positionTitle,
            string structuralUnit,
            string link,
            CancellationToken ct = default
            );

        Task SendAdminApprovalEmailAsync(
            string adminEmail,
            string newUsername,
            string newUserEmail,
            Guid approvalToken,
            CancellationToken ct = default
            );
        
        Task SendPasswordResetEmailAsync(
            string userEmail,
            Guid token,
            CancellationToken ct = default
            );
    }
}
