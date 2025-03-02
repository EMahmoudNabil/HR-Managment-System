using WebApplication1.Models;

namespace HR.ManagmentSystem.Services
{
    public interface IGeneralSettingsService
    {
        Task AddGeneralSettingAsync(GeneralSettings generalSettings);
        Task UpdateGeneralSettingAsync(GeneralSettings generalSettings);
        Task<GeneralSettings> GetGeneralSettings();
    }
}
