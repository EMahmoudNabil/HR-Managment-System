using HR.ManagmentSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace HR.ManagmentSystem.Controllers
{
    [Authorize]
    public class GeneralSettingsController(IGeneralSettingsService generalSettingsService) : Controller
    {
        private readonly IGeneralSettingsService _generalSettingsService = generalSettingsService;

        public async Task<IActionResult> Index()
        {
            return View(await _generalSettingsService.GetGeneralSettings());
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(GeneralSettings generalSettings)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(generalSettings);
        //    }
        //    await _generalSettingsService.AddGeneralSettingAsync(generalSettings);
        //    return RedirectToAction(nameof(Index));
        //}
        public async Task<IActionResult> Edit()
        {
            var selectedDays = Enum.GetValues(typeof(WeekDays))
                .Cast<WeekDays>()
                .Select(d => new SelectListItem
                {
                    Value = ((int)d).ToString(), 
                    Text = d.ToString()         
                })
                .ToList();

            ViewBag.SelectedDays = selectedDays;
            var model = await _generalSettingsService.GetGeneralSettings();
            model.WeeklyHolidays1 = (WeekDays)model.WeeklyHolidays1;
            model.WeeklyHolidays1 = (WeekDays)model.WeeklyHolidays1;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GeneralSettings generalSettings)
        {
            if (!ModelState.IsValid)
            {
                return View(generalSettings);
            }
            await _generalSettingsService.UpdateGeneralSettingAsync(generalSettings);
            return RedirectToAction(nameof(Index));
        }
    }
}
