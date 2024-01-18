using Microsoft.AspNetCore.Identity;

namespace Cars_automobile.Models
{
    public class AppUser : IdentityUser
    {
        public string Fullname { get; set; }
    }
}
