using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
            Id = Guid.CreateVersion7().ToString();
        }
    }
}
