using HotelListing.VSCode.Models;
using HotelListingAPI.VSCode.Models;

namespace HotelListingAPI.VSCode.Contract
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(int? id); 
        Task<TResult> GetAsync<TResult>(int? id); // new refactoring, #59
        Task<List<T>> GetAllAsync();
        Task<List<TResult>> GetAllAsync<TResult>(); // new refactoring, #59
        Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters);
        Task<T> AddAsync(T entity);
        Task<TResult> AddAsync<TSource, TResult>(TSource source); // new refactoring, #59
        Task DeleteAsync(int id);
        Task UpdateAsync(T entity);
        Task UpdateAsync<TSource>(int id, TSource source) where TSource : IBaseDto; // new refactoring, #59
        Task<bool> Exists(int id);
    }
}