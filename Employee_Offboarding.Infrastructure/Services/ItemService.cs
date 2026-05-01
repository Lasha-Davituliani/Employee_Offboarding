using Employee_Offboarding.Application.Abstractions.Repositories;
using Employee_Offboarding.Application.Abstractions.Services;
using Employee_Offboarding.Domain.Entities;

namespace Employee_Offboarding.Infrastructure.Services
{
    public class ItemService : IItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> CreateAsync(int departmentId, string name, ItemKind kind, bool isActive, int displayOrder, CancellationToken ct = default)
        {
            var item = new Item
            {
                DepartmentId = departmentId,
                Name = name,
                Kind = kind,
                IsActive = isActive,
                DisplayOrder = displayOrder
            };
            await _unitOfWork.ItemRepository.AddAsync(item, ct);
            await _unitOfWork.SaveChangesAsync(ct);
            return item.Id;
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var item = await _unitOfWork.ItemRepository.GetByIdAsync(id, true, ct);
            if (item is null)
                throw new Exception("Item not found");
            _unitOfWork.ItemRepository.Remove(item);
            await _unitOfWork.SaveChangesAsync(ct);
        }

        public Task<IReadOnlyList<Item>> ListByDepartmentAsync(int departmentId, CancellationToken ct = default)
            => _unitOfWork.ItemRepository.FindAsync(x => x.DepartmentId == departmentId, true, ct);

        public async Task UpdateAsync(int id, string name, ItemKind kind, bool isActive, int displayOrder, CancellationToken ct = default)
        {
           var item = await _unitOfWork.ItemRepository.GetByIdAsync(id, true, ct);
            if (item is null)
                throw new Exception("Item not found");
            item.Name = name;
            item.Kind = kind;
            item.IsActive = isActive;
            item.DisplayOrder = displayOrder;
            _unitOfWork.ItemRepository.Update(item);
            await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}
