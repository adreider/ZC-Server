namespace TI_Domain.Entities
{
    public class Imagem
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public Empresa IdEmpresa { get; set; }
        public Galeria IdGaleria { get; set; }
        public TipoImagem IdTipoImagem { get; set; }
    }
}