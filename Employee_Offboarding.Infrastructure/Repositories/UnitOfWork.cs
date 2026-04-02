using Employee_Offboarding.Application.Abstractions.Repositories;
using Employee_Offboarding.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;

namespace Employee_Offboarding.Infrastructure.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _context;
        private IDbContextTransaction _transaction;
        private bool _disposed;

        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        private readonly Lazy<IServiceCenterRepository> _serviceCenterRepository;
        private readonly Lazy<IDepartmentResponsiblePersonRepository> _departmentResponsiblePersonRepository;
        private readonly Lazy<IServiceCenterResponsiblePersonRepository> _serviceCenterResponsiblePersonRepository;
        private readonly Lazy<IItemRepository> _itemRepository;
        private readonly Lazy<IClearanceFormItemRepository> _clearanceFormItemRepository;
        private readonly Lazy<IClearanceFormRepository> _clearanceFormRepository;
        private readonly Lazy<IClarenceFormDepartmentRepository> _clearanceFormDepartmentRepository;
        private readonly Lazy<IFormAssignmentRepository> _formAssignmentRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_context));
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(_context));
            _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(_context));
            _serviceCenterRepository = new Lazy<IServiceCenterRepository>(() => new ServiceCenterRepository(_context));
            _departmentResponsiblePersonRepository = new Lazy<IDepartmentResponsiblePersonRepository>(() => new DepartmentResponsiblePersonRepository(_context));
            _serviceCenterResponsiblePersonRepository = new Lazy<IServiceCenterResponsiblePersonRepository>(() => new ServiceCenterResponsiblePersonRepository(_context));
            _itemRepository = new Lazy<IItemRepository>(() => new ItemRepository(_context));
            _clearanceFormItemRepository = new Lazy<IClearanceFormItemRepository>(() => new ClearanceFormItemRepository(_context));
            _clearanceFormRepository = new Lazy<IClearanceFormRepository>(() => new ClearanceFormRepository(_context));
            _clearanceFormDepartmentRepository = new Lazy<IClarenceFormDepartmentRepository>(() => new ClearanceFormDepartmentRepository(_context));
            _formAssignmentRepository = new Lazy<IFormAssignmentRepository>(() => new FormAssignmentRepository(_context));
        }



        public IUserRepository UserRepository => _userRepository.Value;

        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

        public IDepartmentRepository DepartmentRepository => _departmentRepository.Value;

        public IServiceCenterRepository ServiceCenterRepository => _serviceCenterRepository.Value;

        public IDepartmentResponsiblePersonRepository DepartmentResponsiblePersonRepository => _departmentResponsiblePersonRepository.Value;

        public IServiceCenterResponsiblePersonRepository ServiceCenterResponsiblePersonRepository => _serviceCenterResponsiblePersonRepository.Value;

        public IItemRepository ItemRepository => _itemRepository.Value;
        public IClearanceFormItemRepository ClearanceFormItemRepository => _clearanceFormItemRepository.Value;

        public IClearanceFormRepository ClearanceFormRepository => _clearanceFormRepository.Value;

        public IClarenceFormDepartmentRepository ClearanceFormDepartmentRepository => _clearanceFormDepartmentRepository.Value;

        public IFormAssignmentRepository FormAssignmentRepository => _formAssignmentRepository.Value;

        public void BeginTransaction()
        {
            if (_transaction != null) throw new InvalidOperationException("A transaction is already in progress.");
            _transaction = _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (_transaction == null) throw new InvalidOperationException("Transaction is not started.");

            try
            {
                SaveChanges();
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public void RollbackTransaction()
        {
            if (_transaction == null) throw new InvalidOperationException("Transaction is not started.");
            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;
        }

        public int SaveChanges() => _context.SaveChanges();
        public Task<int> SaveChangesAsync(CancellationToken ct = default) => _context.SaveChangesAsync(ct);
        private void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _transaction?.Dispose();
                _context?.Dispose();
            }

            _transaction = null;
            _context = null!;
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
