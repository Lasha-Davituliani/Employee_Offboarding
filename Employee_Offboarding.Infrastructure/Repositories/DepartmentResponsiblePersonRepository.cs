using Employee_Offboarding.Application.Abstractions.Repositories;
using Employee_Offboarding.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employee_Offboarding.Infrastructure.Repositories
{
    public class DepartmentResponsiblePersonRepository : BaseRepository<DepartmentResponsiblePerson>, IDepartmentResponsiblePersonRepository
    {
        public DepartmentResponsiblePersonRepository(DbContext context) : base(context) { }
    }
}
