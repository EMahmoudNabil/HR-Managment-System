using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Helpers;

namespace WebApplication1.Models
{
    public class Attendance
    {
        public int ID { get; set; }
        [ForeignKey(nameof(Employee))]
        public string EmployeeID { get; set; } = string.Empty;
        virtual public ApplicationUser Employee { get; set; } = default!;
        public DateOnly Date { get; set; }
        public TimeOnly? TimeIn { get; set; }
        public TimeOnly? TimeOut { get; set; }
        public AttendanceStatus AttendanceStatus { get; set; }
    }
}
