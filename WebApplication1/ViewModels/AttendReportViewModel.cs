namespace HR.ManagmentSystem.ViewModels
{
    public class AttendReportViewModel
    {

        public int ID { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public string DepartmentName { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly? TimeIn { get; set; }
        public TimeOnly? TimeOut { get; set; }
        public string AttendanceStatus { get; set; }
    }
}
