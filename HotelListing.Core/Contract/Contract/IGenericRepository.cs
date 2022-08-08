using HotelListingAPI.VSCode.Models;

namespace HotelListingAPI.VSCode.Contract
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(int? id); 
        Task<List<T>> GetAllAsync();
        Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters);
        Task<T> AddAsync(T entity);
        Task DeleteAsync(int id);
        Task UpdateAsync(T entity);
        Task<bool> Exists(int id);
    }
}