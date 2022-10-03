using Google.Protobuf.WellKnownTypes;
using Locadora_WebAPI_DotNet.Model;
using Locadora_WebAPI_DotNet.Objeto;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Locadora_WebAPI_DotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        // GET: api/<ClienteController>
        [HttpGet("Consultar")]
        public IEnumerable<string> Consultar()
        {
            using (MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=locadora;port=3306;password=password!123"))
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM `cliente`;");
                    cmd.Connection = con;

                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        //buscaaa

                    }
                }
                finally
                {
                    con.Close();
                }

            }

            return new string[] { "value1", "value2" };
        }

        // GET api/<ClienteController>/5
        [HttpGet("Consultar/{id}")]
        public string Consultar(int id)
        {
            using (MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=locadora;port=3306;password=password!123"))
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM `cliente` WHERE Id = @Id;");
                    cmd.Parameters.Add("@Id", MySqlDbType.Int64);
                    cmd.Parameters["@Id"].Value = id;
                    cmd.Connection = con;

                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        //buscaaa
                        
                    }
                }
                finally
                {
                    con.Close();
                }

            }
        
            return "value";
        }

        // POST api/<ClienteController>
        [HttpPost("Cadastrar")]
        public void Cadastrar([FromBody] Cliente value)
        {
            using(MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=locadora;port=3306;password=password!123"))
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO `cliente` (`Nome`,`CPF`,`DataNascimento`) values( @Nome, @CPF, @DataNascimento);");
                    cmd.Parameters.Add("@Nome", MySqlDbType.VarChar);
                    cmd.Parameters.Add("@CPF", MySqlDbType.VarChar);
                    cmd.Parameters.Add("@DataNascimento", MySqlDbType.DateTime);
                    cmd.Parameters["@Nome"].Value = value.Nome;
                    cmd.Parameters["@CPF"].Value = value.CPF;
                    cmd.Parameters["@DataNascimento"].Value = value.DataNascimento;
                    cmd.Connection = con;

                    MySqlDataReader reader = cmd.ExecuteReader();
                }
                finally
                {
                    con.Close();
                }

            }

        }

        // PUT api/<ClienteController>/5
        [HttpPut("Atualizar/{id}")]
        public void Atualizar(int id, [FromBody] Cliente value)
        {
            using (MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=locadora;port=3306;password=password!123"))
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE `cliente` SET `Nome` = @Nome, `CPF` = @CPF,`DataNascimento` = @DataNascimento WHERE `Id` = @Id;");
                    cmd.Parameters.Add("@Nome", MySqlDbType.VarChar);
                    cmd.Parameters.Add("@CPF", MySqlDbType.VarChar);
                    cmd.Parameters.Add("@DataNascimento", MySqlDbType.DateTime);
                    cmd.Parameters.Add("@Id", MySqlDbType.Int64);
                    cmd.Parameters["@Nome"].Value = value.Nome;
                    cmd.Parameters["@CPF"].Value = value.CPF;
                    cmd.Parameters["@DataNascimento"].Value = value.DataNascimento;
                    cmd.Parameters["@Id"].Value = id;
                    cmd.Connection = con;

                    MySqlDataReader reader = cmd.ExecuteReader();
                }
                finally
                {
                    con.Close();
                }

            }
        }

        // DELETE api/<ClienteController>/5
        [HttpDelete("Deletar/{id}")]
        public void Excluir(int id)
        {
            using (MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=locadora;port=3306;password=password!123"))
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM `cliente` WHERE Id = @Id;");
                    cmd.Parameters.Add("@Id", MySqlDbType.Int64);
                    cmd.Parameters["@Id"].Value = id;
                    cmd.Connection = con;

                    MySqlDataReader reader = cmd.ExecuteReader();
                }
                finally
                {
                    con.Close();
                }

            }
        }
    }
}
