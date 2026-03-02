namespace Employee_Offboarding.Application.DTOs.Forms
{
    public sealed record FormRecipientsEditDto
    (
        int ClearanceFormId,
        IEnumerable<(int DepartmertId, int? DepartmentResponsiblePersonId)> DepartmentRecipients,
        IEnumerable<(int ServiceCenterId, int? ServiceCenterResponsiblePersonId)> ServiceCenterRecipients,
        bool ResendLinks
        );
}
