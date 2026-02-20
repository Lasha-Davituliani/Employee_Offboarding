using System.Linq.Expressions;

namespace Employee_Offboarding.Application.Abstractions.Repositories
{
    public interface IBaseRepository<T> where T :class
    {
        Task<T?> GetByIdAsync(int id, bool isNoTracking = true, CancellationToken ct = default);
        Task<IReadOnlyList<T>> GetAllAsync(bool isNoTracking = true, CancellationToken ct = default);
        Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = true, CancellationToken ct = default);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken ct = default);

        Task AddAsync(T entity, CancellationToken ct = default);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default);

        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

        IQueryable<T> Query();

    }
}
