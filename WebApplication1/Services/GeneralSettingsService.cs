using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace HR.ManagmentSystem.Services
{
    public class GeneralSettingsService(ApplicationDbContext context) : IGeneralSettingsService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task AddGeneralSettingAsync(GeneralSettings generalSettings)
        {
            var existingSetting = await _context.GeneralSettings.FirstOrDefaultAsync();

            if (existingSetting != null)
            {
                _context.GeneralSettings.Remove(existingSetting);
            }

            await _context.GeneralSettings.AddAsync(generalSettings);
            await _context.SaveChangesAsync();
        }

        public async Task<GeneralSettings> GetGeneralSettings()
        {
            return await _context.GeneralSettings.FirstOrDefaultAsync();
        }

        public async Task UpdateGeneralSettingAsync(GeneralSettings generalSettings)
        {
            var existingSetting = await GetGeneralSettings();
            existingSetting.WeeklyHolidays1 = generalSettings.WeeklyHolidays1;
            existingSetting.WeeklyHolidays2 = generalSettings.WeeklyHolidays2;
            existingSetting.DeductionRatePerHour = generalSettings.DeductionRatePerHour;
            existingSetting.OvertimeRatePerHour = generalSettings.OvertimeRatePerHour;
            _context.Update(existingSetting);
            await _context.SaveChangesAsync();
        }
    }
}
