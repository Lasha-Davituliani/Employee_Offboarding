using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Offboarding.Application.Abstractions.Services
{
    public interface IWorkflowService
    {
        Task CheckFormStateAndProgressAsync(int formId, CancellationToken ct = default);
        Task ConfirmByEmployeeAsync(Guid token, bool agreed, string? comment, CancellationToken ct = default);
        Task FinalizeByHrAsync(int formId, int hrUseId, CancellationToken ct = default);
        Task ForceConfirmeByHrAsync(int formId, int hrUseId, CancellationToken ct = default);
    }
}
