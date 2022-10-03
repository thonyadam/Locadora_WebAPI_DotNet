namespace Locadora_WebAPI_DotNet.Model
{
    public class ClienteModel
    {
        //Indexes idx_Cpf idx_Nome
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
