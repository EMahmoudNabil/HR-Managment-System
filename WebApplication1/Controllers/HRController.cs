using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class HRController(IDepartmentService departmentService) : Controller
    {
        private readonly IDepartmentService _departmentService = departmentService;

        public async Task<IActionResult> Index()
        {
            return View(await _departmentService.GetDepartmentListAsync());
        }

        public IActionResult Create()
        {
            return View(model: new Department());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if(!ModelState.IsValid)
                return View(department);

            await _departmentService.AddDepartmentAsync(department);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View(await _departmentService.GetDepartmentByIdAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Department department)
        {
            if(!ModelState.IsValid)
                return View(department);

            await _departmentService.UpdateDepartmentAsync(department);
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _departmentService.DeleteDepartmentAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
