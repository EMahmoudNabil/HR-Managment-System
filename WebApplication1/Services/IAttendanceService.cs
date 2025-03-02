using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApplication1.Models;

namespace HR.ManagmentSystem.Services
{
    public interface IAttendanceService
    {
        List<Attendance> GetAllRecords();
        List<ApplicationUser> GetEmployees();
        ApplicationUser? GetEmployeeById(string employeeId);
        Attendance? GetTodayRecord(string employeeId);
        string CheckIn(string employeeId, DateOnly selectedDate, TimeOnly inputTime);
        string CheckOut(string employeeId, DateOnly selectedDate, TimeOnly inputTime);
        Attendance GetAttendanceById(int id);
        void UpdateAttendance(Attendance attendance);
        string DeleteAttendance(int id);
        List<Attendance> GetAttendanceForEmployee(string employeeId, int year, int month);
        List<ApplicationUser> GetEmployeesByDepartment(int departmentId);
 
    }
}
