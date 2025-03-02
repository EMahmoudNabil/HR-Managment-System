namespace HR.ManagmentSystem.ViewModels
{
    public class EmployeeSalaryViewModel
    {
       

        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public decimal BasicSalary { get; set; }
        public int PresentDays { get; set; }
        public int AbsentDays { get; set; }
        public decimal OvertimeHours { get; set; }
        public decimal DeductionHours { get; set; }
        public decimal TotalOvertimePay { get; set; }
        public decimal TotalDeduction { get; set; }
        public decimal NetSalary { get; set; }
        public int SelectedMonth { get; set; }
        public int SelectedYear { get; set; }
    }
}
//public string Emp_Name { get; set; }
//public string Dep_Name { get; set; }
//public decimal BasicSalary { get; set; }
//public int AttendanceDays { get; set; }
//public int AbsenceDays { get; set; }
//public int OvertimeHours { get; set; }
//public decimal OvertimeAmount { get; set; }
//public int DeductionHours { get; set; }
//public decimal DeductionAmount { get; set; }
//public decimal TotalDeductions { get; set; }
//public decimal NetSalary { get; set; }
