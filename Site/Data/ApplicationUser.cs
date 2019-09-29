using Microsoft.AspNetCore.Identity;

namespace Site.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Account { get; set; }
    }
}
