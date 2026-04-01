using Employee_Offboarding.Application.Abstractions.Repositories;
using Employee_Offboarding.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employee_Offboarding.Infrastructure.Repositories
{
    public class ClearanceFormRepository : BaseRepository<ClearenceForm>, IClearanceFormRepository
    {
        public ClearanceFormRepository(DbContext context) : base(context) { }
    }
}
