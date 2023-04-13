using Microsoft.AspNetCore.Identity;

namespace HamburgerMVC.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            Orders = new HashSet<Order>();
        }
        public ICollection<Order> Orders { get; set; }
    }
}
