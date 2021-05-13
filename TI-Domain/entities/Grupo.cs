using System;

namespace TI_Domain.Entities
{
    public class Grupo
    {
        public int Id { get; set; }
        public Empresa IdEmpresa { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}