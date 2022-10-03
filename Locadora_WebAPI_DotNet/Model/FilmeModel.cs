namespace Locadora_WebAPI_DotNet.Model
{
    public class FilmeModel
    {
        public FilmeModel()
        {
        }

        public FilmeModel(int id, string titulo, int classificacaoIndicativa, int lancamento)
        {
            this.id = id;
            this.titulo = titulo;
            this.classificacaoIndicativa = classificacaoIndicativa;
            this.lancamento = lancamento;
        }

        public int id { get; set; }
        public string titulo { get; set; }
        public int classificacaoIndicativa { get; set; }
        public int lancamento { get; set; }
    }
}
