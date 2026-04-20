using Employee_Offboarding.Application.Abstractions.Repositories;
using Employee_Offboarding.Application.Abstractions.Services;
using Employee_Offboarding.Domain.Entities;

namespace Employee_Offboarding.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _uow;
        public EmployeeService(IUnitOfWork uow) => _uow = uow;
        public async Task<int> CreateAsync(string firstName, string lastName, string email, string personlNumber, string? positionTitle, int? departmentId, int? serviceCenterId, string directManagerName, string directManagerEmail, CancellationToken ct = default)
        {
            if((departmentId.HasValue && serviceCenterId.HasValue)||(!departmentId.HasValue && !serviceCenterId.HasValue))
            {
                throw new ArgumentException("Either departmentId or serviceCenterId must be provided, but not both.");
            }

            var e = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PersonalNumber = personlNumber,
                PositionTitle = positionTitle,
                DepartmentId = departmentId,
                ServiceCenterId = serviceCenterId,
                DirectManagerName = directManagerName,
                DirectManagerEmail = directManagerEmail
            };

            await _uow.EmployeeRepository.AddAsync(e, ct);
            await _uow.SaveChangesAsync(ct);
            return e.Id;

        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var e = await _uow.EmployeeRepository.GetByIdAsync(id, false, ct) ?? throw new Exception("Employee not found");
            _uow.EmployeeRepository.Remove(e);
            await _uow.SaveChangesAsync(ct);
        }

        public Task<Employee?> GetAsync(int id, CancellationToken ct = default)=> _uow.EmployeeRepository.GetByIdAsync(id, true, ct);

        public Task<IReadOnlyList<Employee>> ListAsync(CancellationToken ct = default)=> _uow.EmployeeRepository.GetAllAsync(true, ct);

        public async Task UpdateAsync(int id, string firstName, string lastName, string personalNumber, int? departmentId, int? serviceCenterId, CancellationToken ct = default)
        {
            var e = await _uow.EmployeeRepository.GetByIdAsync(id, false, ct) ?? throw new Exception("Employee not found");
            var xor = (departmentId.HasValue ? 1 : 0) + (serviceCenterId.HasValue ? 1 : 0);
            if(xor !=1) throw new ArgumentException("Either departmentId or serviceCenterId must be provided, but not both.");

            e.FirstName = firstName;
            e.LastName = lastName;
            e.PersonalNumber = personalNumber;
            e.DepartmentId = departmentId;
            e.ServiceCenterId = serviceCenterId;

            _uow.EmployeeRepository.Update(e);
            await _uow.SaveChangesAsync(ct);
        }
    }
}
