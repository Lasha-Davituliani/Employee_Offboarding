using Employee_Offboarding.Application.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Employee_Offboarding.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;
        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public Task AddAsync(T entity, CancellationToken ct = default) => _dbSet.AddAsync(entity, ct).AsTask();

        public Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default) => _dbSet.AddRangeAsync(entities, ct);

        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)=> _dbSet.AnyAsync(predicate, ct);

        public Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken ct = default) => predicate is null ? _dbSet.CountAsync(ct)
            : _dbSet.CountAsync(predicate, ct);

        public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = true, CancellationToken ct = default) => asNoTracking
            ? await _dbSet.AsNoTracking().Where(predicate).ToListAsync(ct)
            : await _dbSet.Where(predicate).ToListAsync(ct);


        public async Task<IReadOnlyList<T>> GetAllAsync(bool isNoTracking = true, CancellationToken ct = default) => isNoTracking
            ? await _dbSet.AsNoTracking().ToListAsync(ct)
            : await _dbSet.ToListAsync(ct);


        public async Task<T?> GetByIdAsync(int id, bool isNoTracking = true, CancellationToken ct = default)
        {
            if (isNoTracking)
            {
                return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id, ct);
            }
            return await _dbSet.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id, ct);
        }

        public IQueryable<T> Query() => _dbSet;

        public void Remove(T entity) => _dbSet.Remove(entity);


        public void RemoveRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);

        public void Update(T entity) => _dbSet.Update(entity);
    }
}
