using Employee_Offboarding.Application.Abstractions.Repositories;
using Employee_Offboarding.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employee_Offboarding.Infrastructure.Repositories
{
    public class ClearanceFormItemRepository : BaseRepository<ClearenceFormItem>, IClearanceFormItemRepository
    {
        public ClearanceFormItemRepository(DbContext context) : base(context) { }
    }
}
