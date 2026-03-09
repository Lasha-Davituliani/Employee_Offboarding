namespace Employee_Offboarding.Application.Abstractions.Services
{
    public interface ILinkTokenService
    {
        Guid NewToken();
        string BuildModuleUrl(Guid token, string? absoluteBaseUrl = null);
    }
}
