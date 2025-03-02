using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IRepositoryService <T> where T : class
    {
       public Task<IList<T>> GetEmployeeListAsync();
        Task<T> GetEmployeeByIdAsync(string id);
        Task AddEmployeeAsync(T entity);
        Task UpdateEmployeeAsync(T entity);
        Task DeleteEmployeeAsync(string id);
    }
}
