using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class PublicHoliday
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; } =string.Empty;
        public DateOnly Date { get; set; }

        [ForeignKey(nameof(GeneralSettings))]
        public int GeneralSettingsId { get; set; }
        [JsonIgnore]
        virtual public GeneralSettings GeneralSettings { get; set; }


    }
}
