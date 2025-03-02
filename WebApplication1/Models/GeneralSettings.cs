
using WebApplication1.Helpers;

namespace WebApplication1.Models
{
    public class GeneralSettings
    {
        public int Id { get; set; }

        public decimal OvertimeRatePerHour { get; set; } 

        public decimal DeductionRatePerHour { get; set; } 

        public WeekDays WeeklyHolidays1 { get; set; } 
        public WeekDays WeeklyHolidays2 { get; set; } 


        virtual public ICollection<PublicHoliday>? PublicHolidays { get; set; }

    }
}
