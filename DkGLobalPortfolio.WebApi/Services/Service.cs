using DkGLobalPortfolio.WebApi.Database;
using DkGLobalPortfolio.WebApi.Models.Request;
using DkGLobalPortfolio.WebApi.Services.IServices;
using DkGLobalPortfolio.WebApi.Utilities;
using Microsoft.EntityFrameworkCore;

namespace DkGLobalPortfolio.WebApi.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly DkGlobalPortfolioDbContext _db;
        private readonly DbSet<T> _dbSet;

        public Service(DkGlobalPortfolioDbContext db)
        {
            _db = db;
            this._dbSet = db.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(GenericServiceRequest<T> request)
        {
            IQueryable<T> query = request.NoTracking ? _dbSet.AsNoTracking() : _dbSet;
            if (request.Expression != null)
            {
                return await query.AnyAsync(request.Expression, request.CancellationToken);
            }
            return await query.AnyAsync(request.CancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsync(GenericServiceRequest<T> request)
        {
            IQueryable<T> query = request.NoTracking ? _dbSet.AsNoTracking() : _dbSet;
            if (request.Expression != null)
            {
                query = query.Where(request.Expression);
            }
            if (request.IncludeProperties != null)
            {
                foreach (var includeProperty in request.IncludeProperties.Split([","], StringSplitOptions.RemoveEmptyEntries))
                {

                    query = query.Include(includeProperty.Trim());

                }
                
            }
            if(request.OrderType != null)
            {
                query = request.OrderType == OrderTypeClass.OrderType.Ascending ? query.OrderBy(request.OrderExpression) : query.OrderByDescending(request.OrderExpression);
            }
            return await query.ToListAsync(request.CancellationToken);
        }

        public async Task<T> GetAsync(GenericServiceRequest<T> request)
        {
            IQueryable<T> query = request.NoTracking ? _dbSet.AsNoTracking() : _dbSet;
            if (request.Expression != null)
            {
                query = query.Where(request.Expression);
            }
            if (request.IncludeProperties != null)
            {
                foreach (var includeProperty in request.IncludeProperties.Split([","], StringSplitOptions.RemoveEmptyEntries))
                {

                    query = query.Include(includeProperty.Trim());

                }
            }
            if (request.OrderType != null)
            {
                query = request.OrderType == OrderTypeClass.OrderType.Ascending ? query.OrderBy(request.OrderExpression) : query.OrderByDescending(request.OrderExpression);
            }
            return await query.FirstOrDefaultAsync(request.CancellationToken);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}
