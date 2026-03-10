using Employee_Offboarding.Domain.Entities;

namespace Employee_Offboarding.Application.Abstractions.Services
{
    public interface IPermissionService
    {
        bool IsHr(User user);
        bool IsAuditor(User user);
        bool CanCreateForm(User user);
        bool CanFinalize(User user);
    }
}
