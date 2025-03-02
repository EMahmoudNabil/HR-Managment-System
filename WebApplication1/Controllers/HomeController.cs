using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public HomeController(ILogger<HomeController> _logger, EmployeeService _employeeService, IDepartmentService _departmentService)
        {
            this._logger = _logger;
            this._employeeService = _employeeService;
            this._departmentService = _departmentService;

        }
        public async Task<IActionResult> Index()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            var claims = identityClaims.FindFirst(ClaimTypes.NameIdentifier);
            var userID = claims.Value;
            var employeeList = await _employeeService.GetEmployeeListAsync();
            var departmentList = await _departmentService.GetDepartmentListAsync();
            int count = employeeList.Where(e => e.Id != userID).ToList().Count();
            ViewData["Count"] = count;
            int countleave = employeeList.Where(e=> e.isDeleted==true).ToList().Count();
            ViewData["Numberofleave"] = countleave;
            int DepartmentCount = departmentList.Count();
            ViewData["DepartmentCount"] = DepartmentCount;
            int NewEmployeeCount = employeeList.Where(e => e.DateOfContract.Month == DateTime.Now.AddMonths(-1).Month).Count();
            ViewData["NewEmployeeCount"] = NewEmployeeCount;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
