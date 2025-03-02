using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Helpers;

namespace WebApplication1.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Id = Guid.CreateVersion7().ToString();
            SecurityStamp = Guid.CreateVersion7().ToString();
        }
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public Gender Gender { get; set; }
        public string Nationality { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
        public string SSN { get; set; } = string.Empty;
        public DateOnly DateOfContract { get; set; }
        [Column(TypeName = "money")]
        public decimal Salary { get; set; }
        public TimeOnly TimeIn { get; set; }
        public TimeOnly TimeOut { get; set; }
        [ForeignKey(nameof(Department))]
        public int DepartmentID { get; set; }
        virtual public Department? Department { get; set; }
        public bool isDeleted { get; set; }
    }
}
