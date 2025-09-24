using DkGLobalPortfolio.WebApi.Models.Request;

namespace DkGLobalPortfolio.WebApi.Services.IServices
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(GenericServiceRequest<T> request);
        Task<bool> AnyAsync(GenericServiceRequest<T> request);
        Task<T> GetAsync(GenericServiceRequest<T> request);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Remove (T entity);
        void RemoveRange (IEnumerable<T> entities);
    }
}
