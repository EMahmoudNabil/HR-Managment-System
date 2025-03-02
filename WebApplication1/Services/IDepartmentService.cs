using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IDepartmentService
    {
        Task<IList<Department>> GetDepartmentListAsync();
        Task<Department> GetDepartmentByIdAsync(int id);
        Task AddDepartmentAsync(Department department);
        Task UpdateDepartmentAsync(Department department);
        Task DeleteDepartmentAsync(int id);
    }
}
