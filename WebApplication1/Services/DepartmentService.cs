using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class DepartmentService(ApplicationDbContext context) : IDepartmentService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Department> GetDepartmentByIdAsync(int id)
            => await _context.Departments.FindAsync(id);

        public async Task<IList<Department>> GetDepartmentListAsync()
            => await _context.Departments.ToListAsync();

        public async Task UpdateDepartmentAsync(Department department)
        {
            _context.Update(department);
            await _context.SaveChangesAsync();
        }
        public async Task AddDepartmentAsync(Department department)
        {
            await _context.AddAsync(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            _context.Departments.Remove((await GetDepartmentByIdAsync(id)));
            await _context.SaveChangesAsync();
        }
    }
}
