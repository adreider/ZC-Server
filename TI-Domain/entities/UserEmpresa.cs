using TI_Domain.identity;

namespace TI_Domain.Entities {
    public class UserEmpresa {
        public string UserId { get; set; }
        public User User { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
    }
}