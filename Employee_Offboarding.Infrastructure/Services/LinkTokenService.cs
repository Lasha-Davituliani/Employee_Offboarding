using Employee_Offboarding.Application.Abstractions.Services;
using Microsoft.AspNetCore.Http;

namespace Employee_Offboarding.Infrastructure.Services
{
    public class LinkTokenService : ILinkTokenService
    {
        private readonly IHttpContextAccessor _http;
        public LinkTokenService(IHttpContextAccessor http)
        {
            _http = http;
        }

        public Guid NewToken() => Guid.NewGuid();

        public string BuildModuleUrl(Guid token, string? absoluteBaseUrl = null)
        {
            var path = $"/Modules/Fill?token={token}";
            if(!string.IsNullOrEmpty(absoluteBaseUrl))
            {
                return absoluteBaseUrl!.TrimEnd('/') + path;
            }
            var request = _http.HttpContext?.Request;
            if (request is null) return path;
            var origin = $"{request.Scheme}://{request.Host}";
            return origin + path;
        }
        
    }
}
