using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace TI_Domain.identity
{
    public class Role : IdentityRole {
        public List<UserRole> UserRoles { get; set; }

    }
}