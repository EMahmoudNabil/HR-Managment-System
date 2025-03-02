using HR.ManagmentSystem.Services;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebApplication1.Helpers;
using HR.ManagmentSystem.ViewModels;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Authorization;

namespace HR.ManagmentSystem.Controllers
{
    [Authorize]
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        public IActionResult Report(string searchQuery, DateTime? startDate, DateTime? endDate)
        {
            var attendanceList = _attendanceService.GetAllRecords();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                attendanceList = attendanceList.Where(a =>
                    a.Employee != null &&
                    (a.Employee.FullName.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (a.Employee.Department != null && a.Employee.Department.Name.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0))
                ).ToList();
            }

            if (startDate.HasValue)
            {
                attendanceList = attendanceList.Where(a => a.Date.ToDateTime(TimeOnly.MinValue) >= startDate.Value).ToList();
            }
            if (endDate.HasValue)
            {
                attendanceList = attendanceList.Where(a => a.Date.ToDateTime(TimeOnly.MinValue) <= endDate.Value).ToList();
            }

            var viewModel = attendanceList.Select(a => new AttendReportViewModel
            {
                ID = a.ID,
                EmployeeID = a.EmployeeID,
                FullName = a.Employee?.FullName ?? "Not Available",
                DepartmentName = a.Employee?.Department?.Name ?? "Not Available",
                Date = a.Date,
                TimeIn = a.TimeIn,
                TimeOut = a.TimeOut,
                //AttendanceStatus = a.AttendanceStatus
            }).ToList();

            return View(viewModel);
        }

        public IActionResult CheckInOut()
        {
            var employees = _attendanceService.GetEmployees() ?? new List<ApplicationUser>();
            return View(employees ?? new List<ApplicationUser>());
        }

        [HttpPost]
        public IActionResult CheckIn(string employeeId, DateOnly date, TimeOnly checkInTime)
        {
            string message = _attendanceService.CheckIn(employeeId, date, checkInTime);
            TempData["Message"] = message;
            return RedirectToAction("CheckInOut");
        }

        [HttpPost]
        public IActionResult CheckOut(string employeeId, DateOnly date, TimeOnly checkOutTime)
        {
            string message = _attendanceService.CheckOut(employeeId, date, checkOutTime);
            TempData["Message"] = message;
            return RedirectToAction("CheckInOut");
        }

        public IActionResult Edit(int id)
        {
            var attendance = _attendanceService.GetAttendanceById(id);
            if (attendance == null)
            {
                return NotFound();
            }
            return View(attendance);
        }

        [HttpPost]
        public IActionResult Edit(int id, Attendance updatedAttendance)
        {
            if (id != updatedAttendance.ID)
            {
                return BadRequest();
            }

            _attendanceService.UpdateAttendance(updatedAttendance);
            return RedirectToAction("Report");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var result = _attendanceService.DeleteAttendance(id);

            if (result.StartsWith("✅"))
            {
                return Json(new { success = true, message = result });
            }

            return Json(new { success = false, message = result });
        }




        [HttpGet]
        public IActionResult GetEmployeesByDepartment(int departmentId)
        {
            var employees = _attendanceService.GetEmployeesByDepartment(departmentId);

            return Json(employees);
        }


        public IActionResult ExportToExcel()
        {
            var attendanceList = _attendanceService.GetAllRecords();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Attendance Report");
                worksheet.Cells.LoadFromCollection(attendanceList, true);

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Attendance_Report.xlsx");
            }
        }

        public IActionResult ExportToPDF()
        {
            var attendanceList = _attendanceService.GetAllRecords();

            using (MemoryStream stream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(document, stream);
                document.Open();

                iTextSharp.text.Font font = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK);
                PdfPTable table = new PdfPTable(5)
                {
                    WidthPercentage = 100
                };

                table.AddCell(new PdfPCell(new Phrase("ID", font)));
                table.AddCell(new PdfPCell(new Phrase("Employee Name", font)));
                table.AddCell(new PdfPCell(new Phrase("Department", font)));
                table.AddCell(new PdfPCell(new Phrase("Check-in", font)));
                table.AddCell(new PdfPCell(new Phrase("Check-out", font)));

                foreach (var item in attendanceList)
                {
                    table.AddCell(item.EmployeeID.ToString());
                    table.AddCell(item?.Employee?.FullName);
                    table.AddCell(item?.Employee?.Department?.Name);
                    table.AddCell(item?.TimeIn.ToString() ?? "N/A");
                    table.AddCell(item?.TimeOut.ToString() ?? "N/A");
                }

                document.Add(table);
                document.Close();

                return File(stream.ToArray(), "application/pdf", "Attendance_Report.pdf");
            }
        }
    }
}
