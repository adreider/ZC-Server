namespace TI_Domain.Entities
{
    public class Bloco
    {
        public int Id { get; set; }
        public string NumBlocoLote { get; set; }
        public string NumBlocoCasa { get; set; }
        public string Lado { get; set; }
        public Grupo IdGrupo { get; set; }
        public string Observacao { get; set; }
    }
}