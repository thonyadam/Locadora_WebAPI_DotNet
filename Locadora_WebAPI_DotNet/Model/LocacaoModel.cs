namespace Locadora_WebAPI_DotNet.Model
{
    public class LocacaoModel
    {
        public LocacaoModel()
        {
        }
        public LocacaoModel(int id, int idCliente, int idFilme, DateTime dataLocacao)
        {
            this.id = id;
            this.idCliente = idCliente;
            this.idFilme = idFilme;
            this.dataLocacao = dataLocacao;
        }

        public LocacaoModel(int id, int idCliente, int idFilme, DateTime dataLocacao, DateTime dataDevolucao)
        {
            this.id = id;
            this.idCliente = idCliente;
            this.idFilme = idFilme;
            this.dataLocacao = dataLocacao;
            this.dataDevolucao = dataDevolucao;
        }

        public int id { get; set; }
        public int idCliente { get; set; }
        public int idFilme { get; set; }
        public DateTime dataLocacao { get; set; }
        public DateTime? dataDevolucao { get; set; }

    }
}
