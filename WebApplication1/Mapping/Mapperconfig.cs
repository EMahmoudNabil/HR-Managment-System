using AutoMapper;
using HR.ManagmentSystem.ViewModels;
using WebApplication1.Models;

namespace HR.ManagmentSystem.Mapping
{
    public class Mapperconfig : Profile
    {
        
        public Mapperconfig()
        {
            
            CreateMap<ApplicationUser, EmployeeSalaryViewModel>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.BasicSalary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name)) 
                .ForMember(dest => dest.PresentDays, opt => opt.Ignore()) 
                .ForMember(dest => dest.AbsentDays, opt => opt.Ignore()) 
                .ForMember(dest => dest.OvertimeHours, opt => opt.Ignore()) 
                .ForMember(dest => dest.DeductionHours, opt => opt.Ignore())
                .ForMember(dest => dest.TotalOvertimePay, opt => opt.Ignore()) 
                .ForMember(dest => dest.TotalDeduction, opt => opt.Ignore()) 
                .ForMember(dest => dest.NetSalary, opt => opt.Ignore()) 
                .ForMember(dest => dest.SelectedMonth, opt => opt.Ignore()) 
                .ForMember(dest => dest.SelectedYear, opt => opt.Ignore()); 

            
            CreateMap<Attendance, AttendReportViewModel>()
                .ForMember(dest => dest.EmployeeID, opt => opt.MapFrom(src => src.EmployeeID))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Employee.FullName))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Employee.Department.Name))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.TimeIn, opt => opt.MapFrom(src => src.TimeIn))
                .ForMember(dest => dest.TimeOut, opt => opt.MapFrom(src => src.TimeOut))
                .ForMember(dest => dest.AttendanceStatus, opt => opt.MapFrom(src => src.AttendanceStatus.ToString()));

        }
    }

}

