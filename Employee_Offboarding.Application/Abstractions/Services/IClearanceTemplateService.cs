using Employee_Offboarding.Application.DTOs.Forms;

namespace Employee_Offboarding.Application.Abstractions.Services
{
    public interface IClearanceTemplateService
    {
        Task<int> CreateFromTemplateAsync(int employeeId, int initiatedByUserId, bool isServiceCenter, int? serviceCenterId = null, CancellationToken ct = default);
        Task<FormPreviewDto> GetPreviewAsync(bool isServiceCenter,CancellationToken ct = default);
    }
}
