using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Department
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        virtual public ICollection<ApplicationUser> Employees { get; set; } = [];
    }
}
