using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class EmployeeController(EmployeeService _employeeService , IDepartmentService _departmentService) : Controller
    {
       private readonly EmployeeService _employeeService= _employeeService;
        private readonly IDepartmentService _departmentService= _departmentService;


        public async Task<IActionResult> Index()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            var claims = identityClaims.FindFirst(ClaimTypes.NameIdentifier);
            var userID = claims.Value;
            return View((await _employeeService.GetEmployeeListAsync()).Where(e => e.Id != userID).ToList());
        }
        [HttpGet]
        public async Task<IActionResult> Create() {
            var Branchs =await _departmentService.GetDepartmentListAsync();
            ViewBag.Branchs = Branchs.Select(b => new SelectListItem
            {
                Value = b.ID.ToString(),
                Text = b.Name
            }).ToList();
            return View(model: new ApplicationUser());
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationUser Employee)
        {
            if (ModelState.IsValid) {
                await _employeeService.AddEmployeeAsync(Employee);
                return RedirectToAction("Index");
            }
            var Branchs = await _departmentService.GetDepartmentListAsync();
            ViewBag.Branchs = Branchs.Select(b => new SelectListItem
            {
                Value = b.ID.ToString(),
                Text = b.Name
            }).ToList();
            return View(Employee);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var Branchs = await _departmentService.GetDepartmentListAsync();

            ViewData["Branchs"] = new SelectList(Branchs, "ID", "Name", Branchs.Select(I => I.Name));


            return View("Edit", await _employeeService.GetEmployeeByIdAsync(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(ApplicationUser Employee)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.UpdateEmployeeAsync(Employee);
                return RedirectToAction(nameof(Index));
            }
            var Branchs = await _departmentService.GetDepartmentListAsync();
            ViewData["Branchs"] = new SelectList(Branchs, "ID", "Name", Branchs.Select(I => I.Name));

            return View(Employee);

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
