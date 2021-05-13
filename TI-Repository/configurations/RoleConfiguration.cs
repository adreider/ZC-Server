using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TI_Domain.identity;

namespace TI_Repository.configurations {
    public class RoleConfiguration : IEntityTypeConfiguration<Role> {
        public void Configure (EntityTypeBuilder<Role> builder) {
            builder.ToTable ("AspNetRoles");
            builder.Property (r => r.Name)
                .IsRequired ();

            builder.HasData (
                new Role {
                    Id = System.Guid.NewGuid ().ToString (),
                        Name = "Admin"
                }

            );
        }
    }
}