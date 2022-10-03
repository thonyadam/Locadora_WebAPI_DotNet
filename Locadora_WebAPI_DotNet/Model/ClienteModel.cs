namespace Locadora_WebAPI_DotNet.Model
{
    public class ClienteModel
    {
        public ClienteModel()
        {
        }

        public ClienteModel(int id, string nome, string cpf, DateTime dataNascimento)
        {
            this.id = id;
            this.nome = nome;
            this.cpf = cpf;
            this.dataNascimento = dataNascimento;
        }

        //Indexes idx_Cpf idx_Nome
        public int id { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public DateTime dataNascimento { get; set; }

    }
}
