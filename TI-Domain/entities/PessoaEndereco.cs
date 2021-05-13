

using TI_Domain.identity;

namespace TI_Domain.Entities {
    public class PessoaEndereco {
        public int Id { get; set; }
        public User IdUser { get; set; }
        public Endereco IdEndereco { get; set; }
    }
}