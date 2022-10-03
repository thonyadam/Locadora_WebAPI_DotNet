namespace Locadora_WebAPI_DotNet.Model
{
    public class LocacaoModel
    {
        //indexes FK_Cliente_idx FK_Filme_idx
        public int Id { get; set; }
        public int Id_Cliente { get; set; }
        public int Id_Filme { get; set; }
        public DateTime DataLocacao { get; set; }
        public DateTime DataDevolucao { get; set; }

    }
}
