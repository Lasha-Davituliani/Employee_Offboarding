namespace Employee_Offboarding.Application.DTOs.Common
{
    public sealed record PagedQueryDto(int Page = 1, int PageSize = 20, string? Search = null);
    
}
