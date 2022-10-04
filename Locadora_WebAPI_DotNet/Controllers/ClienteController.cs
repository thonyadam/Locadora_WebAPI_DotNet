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
        private readonly IConfiguration Configuration;

        public ClienteController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Lista todos os clientes cadastrados
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("Consultar")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClienteModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Consultar()
        {
            using (MySqlConnection con = new MySqlConnection(Configuration["MysqlPath"]))
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM `cliente`;");
                    cmd.Connection = con;

                    MySqlDataReader reader = cmd.ExecuteReader();

                    var lista = new List<ClienteModel>();

                    while (reader.Read())
                    {
                        var cliente = new ClienteModel(
                            Convert.ToInt32(reader["Id"].ToString()),
                            reader["Nome"].ToString(),
                            reader["CPF"].ToString(),
                            Convert.ToDateTime(reader["DataNascimento"].ToString())
                            );

                        lista.Add(cliente);
                    }

                    return Ok(lista);
                }
                catch
                {
                    return BadRequest();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        /// <summary>
        /// Consulta um cliente pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Consultar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Consultar(int id)
        {
            using (MySqlConnection con = new MySqlConnection(Configuration["MysqlPath"]))
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM `cliente` WHERE Id = @Id;");
                    cmd.Parameters.Add("@Id", MySqlDbType.Int32);
                    cmd.Parameters["@Id"].Value = id;
                    cmd.Connection = con;

                    MySqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();

                    var cliente = new ClienteModel(
                            Convert.ToInt32(reader["Id"].ToString()),
                            reader["Nome"].ToString(),
                            reader["CPF"].ToString(),
                            Convert.ToDateTime(reader["DataNascimento"].ToString())
                            );

                    return Ok(cliente);
                }
                catch 
                {
                    return BadRequest();
                }
                finally
                {
                    con.Close();
                }

            }
        
             ;
        }

        /// <summary>
        /// Cadastra um cliente
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpPost("Cadastrar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Cadastrar([FromBody] Cliente value)
        {
            using(MySqlConnection con = new MySqlConnection(Configuration["MysqlPath"]))
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

                    return Ok();
                }
                catch
                {
                    return BadRequest();
                }
                finally
                {
                    con.Close();
                }

            }

        }

        /// <summary>
        /// Atualiza um cliente pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("Atualizar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Atualizar(int id, [FromBody] Cliente value)
        {
            using (MySqlConnection con = new MySqlConnection(Configuration["MysqlPath"]))
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE `cliente` SET `Nome` = @Nome, `CPF` = @CPF,`DataNascimento` = @DataNascimento WHERE `Id` = @Id;");
                    cmd.Parameters.Add("@Nome", MySqlDbType.VarChar);
                    cmd.Parameters.Add("@CPF", MySqlDbType.VarChar);
                    cmd.Parameters.Add("@DataNascimento", MySqlDbType.DateTime);
                    cmd.Parameters.Add("@Id", MySqlDbType.Int32);
                    cmd.Parameters["@Nome"].Value = value.Nome;
                    cmd.Parameters["@CPF"].Value = value.CPF;
                    cmd.Parameters["@DataNascimento"].Value = value.DataNascimento;
                    cmd.Parameters["@Id"].Value = id;
                    cmd.Connection = con;

                    MySqlDataReader reader = cmd.ExecuteReader();

                    return Ok();
                }
                catch
                {
                    return BadRequest();
                }
                finally
                {
                    con.Close();
                }

            }
        }

        /// <summary>
        /// Deleta um cliente pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Deletar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Excluir(int id)
        {
            using (MySqlConnection con = new MySqlConnection(Configuration["MysqlPath"]))
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM `cliente` WHERE Id = @Id;");
                    cmd.Parameters.Add("@Id", MySqlDbType.Int32);
                    cmd.Parameters["@Id"].Value = id;
                    cmd.Connection = con;

                    MySqlDataReader reader = cmd.ExecuteReader();

                    return Ok();
                }
                catch
                {
                    return BadRequest();
                }
                finally
                {
                    con.Close();
                }

            }
        }
    }
}
