using Employee_Offboarding.Application.Abstractions.Repositories;
using Employee_Offboarding.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employee_Offboarding.Infrastructure.Repositories
{
    public class FormAssignmentRepository : BaseRepository<FormAssignment>, IFormAssignmentRepository
    {
        public FormAssignmentRepository(DbContext context) : base(context) { }
    }
}
