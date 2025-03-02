using HR.ManagmentSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication1.Models;

namespace HR.ManagmentSystem.Controllers
{
    [Authorize]
    public class OfficialHolidaysController(IpublicHolidays _IpublicHolidays) : Controller
    {
        private readonly IpublicHolidays _IpublicHolidays = _IpublicHolidays;

        public async Task<IActionResult> Index()
        {
            return View(await _IpublicHolidays.GetPublicHoliday());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPublicHoliday([FromBody] PublicHoliday publicHoliday)
        {
            //if (!ModelState.IsValid || publicHoliday == null )
            //{
            //    return View(publicHoliday);
            //}
            await _IpublicHolidays.AddPublicHolidaysAsync(publicHoliday);
            return Ok(publicHoliday);
        }

        [HttpGet]
        public async Task<IActionResult> GetHoliday(int id)
        {
            var holiday = await _IpublicHolidays.GetPublicHolidayByIDAsync(id);
            if (holiday == null)
            {
                return NotFound();
            }
            return Ok(holiday);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody] PublicHoliday publicHoliday)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            await _IpublicHolidays.UpdatePublicHolidaysAsync(publicHoliday);
            return Ok(publicHoliday);
        }




        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
               await _IpublicHolidays.DeletePublicHolidayAsync(id);
            return Ok(new { message = "Holiday deleted successfully" });
        }
    }
}

