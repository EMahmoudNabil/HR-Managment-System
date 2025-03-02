using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace HR.ManagmentSystem.Services
{
    public class PublicHolidays(ApplicationDbContext context) : IpublicHolidays
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<PublicHoliday> GetPublicHolidayByIDAsync(int id)
         => await _context.publicHolidays.FindAsync(id);

        public async Task<IList<PublicHoliday>> GetPublicHoliday()
        {
            return await _context.publicHolidays.ToListAsync();
        }

        public async Task AddPublicHolidaysAsync(PublicHoliday publicholiday)
        {
            var generalSetting = _context.GeneralSettings.FirstOrDefault();
            if (generalSetting == null)
            {
                throw new InvalidOperationException("General settings not found.");
            }

            PublicHoliday model = new PublicHoliday();
            model.Id = publicholiday.Id;
            model.Name = publicholiday.Name;
            model.Date = publicholiday.Date;
            model.GeneralSettingsId = generalSetting.Id;

            await _context.publicHolidays.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePublicHolidayAsync(int id)
        {
            _context.publicHolidays.Remove((await GetPublicHolidayByIDAsync(id)));
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePublicHolidaysAsync(PublicHoliday publicHoliday)
        {
            var generalSetting = _context.GeneralSettings.FirstOrDefault();
            if (generalSetting == null)
            {
                throw new InvalidOperationException("General settings not found.");
            }

            var existingHoliday = await _context.publicHolidays.FindAsync(publicHoliday.Id);
            if (existingHoliday != null)
            {
                existingHoliday.Name = publicHoliday.Name;
                existingHoliday.Date = publicHoliday.Date;
                existingHoliday.GeneralSettingsId = generalSetting.Id;
                await _context.SaveChangesAsync();
            }
        }
    }
}
