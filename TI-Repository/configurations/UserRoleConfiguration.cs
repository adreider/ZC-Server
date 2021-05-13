using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TI_Domain.identity;

namespace TI_Repository.configurations {
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole> {
        public void Configure (EntityTypeBuilder<UserRole> userRole) {
            userRole.HasKey (ur => new { ur.UserId, ur.RoleId });
            userRole.HasOne (ur => ur.Role)
                .WithMany (r => r.UserRoles)
                .HasForeignKey (ur => ur.RoleId)
                .IsRequired ();

            userRole.HasOne (ur => ur.User)
                .WithMany (u => u.UserRoles)
                .HasForeignKey (ur => ur.UserId)
                .IsRequired ();
        }
    }
}