using AutoMapper;
using HR.ManagmentSystem.Helpers;
using HR.ManagmentSystem.Services;
using HR.ManagmentSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using WebApplication1.Models;
using WebApplication1.Services;

namespace HR.ManagmentSystem.Controllers
{
    [Authorize]
    public class EmployeeSalaryController(EmployeeService employeeService,
        IGeneralSettingsService generalSettingsService, IpublicHolidays  publicHolidaysService, IAttendanceService attendanceService, IMapper mapper) : Controller
    {
        
        private readonly IMapper _mapper=mapper;
        private readonly EmployeeService _employeeService = employeeService;
        private readonly IGeneralSettingsService _generalSettingsService = generalSettingsService;
        private readonly IpublicHolidays _publicHolidaysService = publicHolidaysService;
        private readonly IAttendanceService _attendanceService = attendanceService;

        public async Task<IActionResult> Index()
        {

            var Employees =await _employeeService.GetEmployeeListAsync();
            List<EmployeeSalaryViewModel> EmployeeViewModel = _mapper.Map<List<EmployeeSalaryViewModel>>(Employees);

            return View(EmployeeViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> EmployeeReport(string employeeId, int? month, int? year)
        {
            //Get Year
            int selectedYear = year ?? DateTime.Now.Year;
            //Get Month
            int selectedMonth = month ?? DateTime.Now.Month;
            //Get General_Settings From DataBase (WeaklyHoliday)
            var generalSettings = await _generalSettingsService.GetGeneralSettings();
            //Get Public Holidays From DataBase (PublicHolidays)
            var publicHolidays = await _publicHolidaysService.GetPublicHoliday();
            //Get Public Holidays in Selected Month And Year
            var publicHolidaysInMonth = publicHolidays
               .Where(ph => ph.Date.Year == selectedYear && ph.Date.Month == selectedMonth)
               .ToList();
            //Calculate Working Days Of Month
            int totalWorkingDays = WorkingDaysCalculate.CalculateWorkingDays(selectedYear, selectedMonth, generalSettings, publicHolidaysInMonth);
            //Get Employee From DataBase
            var employees = await _employeeService.GetEmployeeListAsync();
            //Get Employee By Id From DataBase
            var employee = employees.FirstOrDefault(e => e.Id == employeeId);
            //create List of EmployeeSalaryViewModel (ViewModel)
            var reportData = new List<EmployeeSalaryViewModel>();
            //Get Attendance For Employee From DataBase
            var attendanceRecords = _attendanceService.GetAttendanceForEmployee(employeeId, selectedYear, selectedMonth);
            //Calculate Present Days
            int presentDays = attendanceRecords.Count;
            //Calculate Total Absent Days
            int absentDays = totalWorkingDays - presentDays;
            //create Varibale For Total Overtime Hours
            double totalOvertimeHours = 0;
            //create Varibale For Total Deduction Hours
            double totalDeductionHours = 0;

            //loop In attendanceRecords          
            foreach (var record in attendanceRecords)
            {
                //Get Scheduled Start Time
                var scheduledStart = employee.TimeIn;
                //Get Scheduled End Time
                var scheduledEnd = employee.TimeOut;
                //Calculate Scheduled Duration
                TimeSpan scheduledDuration = scheduledEnd - scheduledStart;
                //convert scheduledDuration To Number Of Hours
                double scheduledHours = scheduledDuration.TotalHours;
                //Get Actual Start Time
                TimeOnly actualStart = record.TimeIn.Value;
                //Get Actual End Time
                TimeOnly actualEnd = record.TimeOut.Value;
                //Calculate Actual Duration
                TimeSpan actualDuration = (actualEnd - actualStart);
                //convert actualDuration To Number Of Hours
                double actualHours = actualDuration.TotalHours;
                //Check Overtime
                if (actualHours > scheduledHours)
                {
                    //Calculate Overtime Hours  
                    totalOvertimeHours += actualHours - scheduledHours;
                }
                else if (actualHours < scheduledHours)
                {
                    //Calculate Deduction Hours
                    totalDeductionHours += scheduledHours - actualHours;
                }
            }
            //Calculate Salary Per Day
            decimal SalaryperDay = ((employee.Salary / totalWorkingDays) == 0) ? 0 : (employee.Salary / totalWorkingDays);
            //Calculate Actual Hours Of Employee
            TimeSpan TotalOverHour = employee.TimeOut - employee.TimeIn;
            //Calculate Over Hour
            decimal OverHour = TotalOverHour.Hours + (TotalOverHour.Minutes / 60);
            //Calculate Amount Per Hour
            decimal AmountPerHour = SalaryperDay / OverHour;
            //Calculate Total Overtime Pay
            decimal totalOvertimePay = ((decimal)totalOvertimeHours * generalSettings.OvertimeRatePerHour) * AmountPerHour;
            //Calculate Total Deduction
            decimal totalDeduction = ((decimal)totalDeductionHours * generalSettings.DeductionRatePerHour)* AmountPerHour;
            //Calculate Net Salary Of Employee
            decimal netSalary = employee.Salary + (totalOvertimePay) - (totalDeduction) - (absentDays*SalaryperDay);

            //Map to ViewModel
            EmployeeSalaryViewModel salaryViewModel = _mapper.Map<EmployeeSalaryViewModel>(employee);

            salaryViewModel.PresentDays = presentDays;
            salaryViewModel.AbsentDays = absentDays;
            salaryViewModel.OvertimeHours = (decimal)totalOvertimeHours;
            salaryViewModel.DeductionHours = (decimal)totalDeductionHours;
            salaryViewModel.TotalOvertimePay = totalOvertimePay;
            salaryViewModel.TotalDeduction = totalDeduction;
            salaryViewModel.NetSalary = netSalary;

            return View(salaryViewModel);
        }
    }
}
