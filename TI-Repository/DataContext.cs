using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TI_Domain.Entities;
using TI_Domain.identity;
using TI_Repository.configurations;

namespace TI_Repository {
  public class DataContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>> {
    public DataContext (DbContextOptions<DataContext> options) : base (options) { }
    public DbSet<Endereco> Endereco { get; set; }
    public DbSet<Imagem> Imagem { get; set; }
    public DbSet<Galeria> galeria { get; set; }
    public DbSet<TipoImagem> TipoImagem { get; set; }
    public DbSet<Empresa> Empresa { get; set; }


    protected override void OnModelCreating (ModelBuilder builder) {
      base.OnModelCreating (builder);
      builder.ApplyConfiguration (new RoleConfiguration ());
      builder.ApplyConfiguration (new UserRoleConfiguration ());
      builder.ApplyConfiguration (new UserEmpresaConfiguration ());
    }

  }
}