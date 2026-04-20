using Employee_Offboarding.Application.Abstractions.Repositories;
using Employee_Offboarding.Application.Abstractions.Services;
using Employee_Offboarding.Domain.Entities;

namespace Employee_Offboarding.Infrastructure.Services
{
    public class DepartmentService : IDerpartmentService
    {
        private readonly IUnitOfWork _uow;
        public DepartmentService(IUnitOfWork uow) => _uow = uow;
        public async Task<int> CreateAsync(string name, CancellationToken ct = default)
        {
            var dep = new Department { Name = name, IsActive = true};
            await _uow.DepartmentRepository.AddAsync(dep, ct);
            await _uow.SaveChangesAsync(ct);
            return dep.Id;
        }

        public Task<Department?> GetByIdAsync(int id, CancellationToken ct = default) => _uow.DepartmentRepository.GetByIdAsync(id,true, ct);


        public Task<IReadOnlyList<Department>> ListAsync(CancellationToken ct = default) => _uow.DepartmentRepository.GetAllAsync(true, ct);

        public async Task UpdateAsync(int id, string name, bool isActive, CancellationToken ct = default)
        {
            var dep = await _uow.DepartmentRepository.GetByIdAsync(id, false, ct) ?? throw new Exception("Department not found");
            dep.Name = name;
            dep.IsActive = isActive;
            _uow.DepartmentRepository.Update(dep);
            await _uow.SaveChangesAsync(ct);
        }
    }
}
