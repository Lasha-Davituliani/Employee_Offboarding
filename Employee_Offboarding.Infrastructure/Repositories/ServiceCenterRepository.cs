using Employee_Offboarding.Application.Abstractions.Repositories;
using Employee_Offboarding.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employee_Offboarding.Infrastructure.Repositories
{
    public class ServiceCenterRepository : BaseRepository<ServiceCenter>, IServiceCenterRepository
    {
        public ServiceCenterRepository(DbContext context) : base(context) { }
    }
}
