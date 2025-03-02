using WebApplication1.Models;

namespace HR.ManagmentSystem.Helpers
{
    public class WorkingDaysCalculate
    {
        public static int CalculateWorkingDays(int year, int month, GeneralSettings settings, List<PublicHoliday> publicHolidays)
        {
            var firstDay = new DateOnly(year, month, 1);//1-10-2025
            var lastDay = firstDay.AddMonths(1).AddDays(-1);//30-10-2025
            int workingDays = 0;

            DayOfWeek weeklyHoliday1 = (DayOfWeek) settings.WeeklyHolidays1;
            DayOfWeek weeklyHoliday2 = (DayOfWeek) settings.WeeklyHolidays2;
           
            for (var day = firstDay; day <= lastDay; day = day.AddDays(1))
            {
                if (day.DayOfWeek == weeklyHoliday1 || day.DayOfWeek == weeklyHoliday2)
                {
                    continue;
                }

                if (publicHolidays.Any(ph => ph.Date == day))
                {
                    continue;
                }

                workingDays++;
            }

            return workingDays;
        }
    }
}
