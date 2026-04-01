using Employee_Offboarding.Domain.Entities;
using Employee_Offboarding.Application.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Employee_Offboarding.Infrastructure.Repositories
{
    public class ClearanceFormDepartmentRepository : BaseRepository<ClearenceFormDepartment>, IClarenceFormDepartmentRepository
    {
        public ClearanceFormDepartmentRepository(DbContext context) : base(context) { }
    }
        
}
