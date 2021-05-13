using Microsoft.AspNetCore.Identity;

namespace TI_Domain.identity
{
    public class UserRole : IdentityUserRole<string> {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}