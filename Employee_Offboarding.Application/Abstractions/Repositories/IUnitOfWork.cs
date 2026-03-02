namespace Employee_Offboarding.Application.Abstractions.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        IServiceCenterRepository ServiceCenterRepository { get; }
        IDepartmentResponsiblePersonRepository DepartmentResponsiblePersonRepository { get; }
        IServiceCenterResponsiblePersonRepository ServiceCenterResponsiblePersonRepository { get; }
        IItemRepository ItemRepository { get; }
        IClearanceFormItemRepository ClearanceFormItemRepository { get; }
        IClearanceFormRepository ClearanceFormRepository { get; }
        IClarenceFormDepartmentRepository ClearanceFormDepartmentRepository { get; }
        IFormAssignmentRepository FormAssignmentRepository { get; }
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
