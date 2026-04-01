using Employee_Offboarding.Application.Abstractions.Repositories;
using Employee_Offboarding.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employee_Offboarding.Infrastructure.Repositories
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        public ItemRepository(DbContext context) : base(context) { }
    }
}
