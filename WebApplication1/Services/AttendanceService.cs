using HR.ManagmentSystem.ViewModels;
using WebApplication1.Data;
using WebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Helpers;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HR.ManagmentSystem.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly ApplicationDbContext _context;

        public AttendanceService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Attendance> GetAttendanceForEmployee(string employeeId, int year, int month)
        {
            DateOnly firstDay = new DateOnly(year, month, 1);
            DateOnly lastDay = firstDay.AddMonths(1).AddDays(-1);

            return _context.Attendances
                .Where(a => a.EmployeeID == employeeId
                    && a.Date >= firstDay
                    && a.Date <= lastDay
                    )
                .ToList();
        }

        public List<Attendance> GetAllRecords()
        {
            return _context.Attendances
                .Include(a => a.Employee) 
                .ThenInclude(e => e.Department)
                .Where(a => a.Employee != null && a.Employee.isDeleted == false) 
                .ToList();
        }

      
        public List<ApplicationUser> GetEmployees()
        {
            return _context.Users.OfType<ApplicationUser>().ToList();
        }

        public ApplicationUser? GetEmployeeById(string employeeId)
        {
            return _context.Users.OfType<ApplicationUser>().SingleOrDefault(e => e.Id == employeeId.ToString());
        }

        public Attendance? GetTodayRecord(string employeeId)
        {
            string employeeIdStr = employeeId.ToString();
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);

            return _context.Attendances
                .SingleOrDefault(a => a.EmployeeID == employeeIdStr && a.Date == today);
        }

        public string CheckOut(string employeeId, DateOnly selectedDate, TimeOnly inputTime)
        {
            var employee = _context.Users.OfType<ApplicationUser>()
                .FirstOrDefault(e => e.Id == employeeId.ToString());

            if (employee == null)
            {
                return "Employee not found!";
            }

            var attendance = _context.Attendances
                .FirstOrDefault(a => a.EmployeeID == employeeId.ToString() && a.Date == selectedDate);

            if (attendance == null)
            {
                return "No check-in record found for today, check-out not possible!";
            }

            if (attendance.TimeOut != null)
            {
                return "Check-out has already been recorded!";
            }

            attendance.TimeOut = inputTime;
            _context.SaveChanges();

            return "Check-out recorded successfully!";
        }

        public string CheckIn(string employeeId, DateOnly selectedDate, TimeOnly inputTime)
        {
            var employee = _context.Users.OfType<ApplicationUser>()
                .FirstOrDefault(e => e.Id == employeeId.ToString());
            AttendanceStatus status = AttendanceStatus.Absent;

            if (employee == null)
            {
                return "Employee not found!";
            }

            var attendance = _context.Attendances
                .FirstOrDefault(a => a.EmployeeID == employeeId.ToString() && a.Date == selectedDate);

            if (attendance != null)
            {
                return "Check-in already recorded for today!";
            }

            if (inputTime > employee.TimeIn)
            {

                attendance = new Attendance
                {
                    EmployeeID = employeeId.ToString(),
                    Date = selectedDate,
                    TimeIn = inputTime,
                    TimeOut = null,
                    AttendanceStatus = AttendanceStatus.Late
                };
                _context.Attendances.Add(attendance);
                _context.SaveChanges();
                return "You are late for the scheduled work time!";
              
            }
      
            attendance = new Attendance
            {
                EmployeeID = employeeId.ToString(),
                Date = selectedDate,
                TimeIn = inputTime,
                TimeOut = null,
                AttendanceStatus = AttendanceStatus.Present
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return "Check-in recorded successfully!";
        }

        public Attendance GetAttendanceById(int id)
        {
            return _context.Attendances.Find(id);
        }

        public void UpdateAttendance(Attendance attendance)
        {
            var existingAttendance = _context.Attendances.Find(attendance.ID);
            if (existingAttendance != null)
            {
                existingAttendance.TimeIn = attendance.TimeIn;
                existingAttendance.TimeOut = attendance.TimeOut;
                existingAttendance.AttendanceStatus = attendance.AttendanceStatus;

                _context.SaveChanges();
            }
        }

   
         public string DeleteAttendance(int id)
        {
            try

            {
                var attendance = _context.Attendances.FirstOrDefault(a => a.ID == id);
                if (attendance == null)
                {
                    return "❌ Error: Record not found!";
                }

                _context.Attendances.Remove(attendance);
                int affectedRows = _context.SaveChanges();

                if (affectedRows > 0)
                {
                    return "✅ Record deleted successfully!";
                }
                else
                {
                    return "❌ Error: No rows were affected!";
                }
            }
            catch (Exception ex)
            {
                return "❌ Exception: " + ex.Message;
            }
        }

        public List<ApplicationUser> GetEmployeesByDepartment(int departmentId)
        {
            return _context.Users.OfType<ApplicationUser>()
                .Where(e => e.DepartmentID == departmentId && e.isDeleted == false) 
                .Select(e => new ApplicationUser
                {
                    Id = e.Id,
                    FullName = e.FullName
                })
                .ToList();
        }

 
        public List<Department> GetAllDepartment()
        {
            return _context.Departments.ToList();
        }
    }
}
