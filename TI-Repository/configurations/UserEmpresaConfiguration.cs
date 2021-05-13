using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TI_Domain.Entities;

namespace TI_Repository.configurations {
    public class UserEmpresaConfiguration : IEntityTypeConfiguration<UserEmpresa> {
        public void Configure (EntityTypeBuilder<UserEmpresa> userEmpresa) {
            userEmpresa.HasKey (ur => new { ur.UserId, ur.EmpresaId });

            userEmpresa.HasOne (ur => ur.Empresa)
                .WithMany (r => r.UserEmpresas)
                .HasForeignKey (ur => ur.EmpresaId)
                .IsRequired ();

            userEmpresa.HasOne (ur => ur.User)
                .WithMany (r => r.UserEmpresas)
                .HasForeignKey (ur => ur.UserId)
                .IsRequired ();
        }
    }
}