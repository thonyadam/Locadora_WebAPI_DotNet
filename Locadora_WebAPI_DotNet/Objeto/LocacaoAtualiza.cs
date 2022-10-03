namespace Locadora_WebAPI_DotNet.Objeto
{
    public class LocacaoAtualiza
    {
        public int idCliente { get; set; }
        public int idFilme { get; set; }
        public DateTime DataLocacao { get; set; }
        public DateTime DataDevolucao { get; set; }
    }
}
