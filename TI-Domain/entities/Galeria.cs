using System;
using TI_Domain.identity;

namespace TI_Domain.Entities
{
    public class Galeria
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Url { get; set; }
        public User IdUser { get; set; }
        public DateTime DataCadastro { get; set; }
        public Empresa IdEmpresa { get; set; }
        public DateTime Data { get; set; }
    }
}