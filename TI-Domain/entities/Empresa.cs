using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TI_Domain.identity;

namespace TI_Domain.Entities {
    public class Empresa {
        [Key]
        public int EmpresaId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Email { get; set; }
        public List<User> User { get; set; }
        public List<UserEmpresa> UserEmpresas { get; set; }
    }
}