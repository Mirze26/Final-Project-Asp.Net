using Microsoft.AspNetCore.Identity;

namespace Payne.Models
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsRemember { get; set; }

    }
}
