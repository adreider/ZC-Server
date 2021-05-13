using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using TI_Domain.Entities;

namespace TI_Domain.identity
{
    public class User : IdentityUser
    {
        [Column (TypeName = "nvarchar(150)")]
        public string FullName { get; set; }
        public string CelWApp { get; set; }
        public string ImgPerfil { get; set; }
        // public string Password { get; set; }
        public List<UserRole> UserRoles { get; set; }
         public List<UserEmpresa> UserEmpresas { get; set; }
    }
}