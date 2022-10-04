using Locadora_WebAPI_DotNet.Model;
using Locadora_WebAPI_DotNet.Objeto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Locadora_WebAPI_DotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocacaoController : ControllerBase
    {
        private readonly IConfiguration Configuration;

        public LocacaoController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Lista todos as locacoes cadastradas
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("Consultar")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LocacaoModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Consultar()
        {
            using (MySqlConnection con = new MySqlConnection(Configuration["MysqlPath"]))
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM `locacao`;");
                    cmd.Connection = con;

                    MySqlDataReader reader = cmd.ExecuteReader();

                    var lista = new List<LocacaoModel>();

                    while (reader.Read())
                    {

                        var locacao = new LocacaoModel(
                            Convert.ToInt32(reader["Id"].ToString()),
                            Convert.ToInt32(reader["Id_Cliente"].ToString()),
                            Convert.ToInt32(reader["Id_Filme"].ToString()),
                            Convert.ToDateTime(reader["DataLocacao"].ToString())
                            );

                        try
                        {
                           locacao.dataDevolucao = Convert.ToDateTime(reader["DataDevolucao"].ToString());
                        }
                        catch { }

                        lista.Add(locacao);
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
        /// Consulta uma locacao pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Consultar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LocacaoModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Consultar(int id)
        {
            using (MySqlConnection con = new MySqlConnection(Configuration["MysqlPath"]))
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM `locacao` WHERE Id = @Id;");
                    cmd.Parameters.Add("@Id", MySqlDbType.Int32);
                    cmd.Parameters["@Id"].Value = id;
                    cmd.Connection = con;

                    MySqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();

                    var locacao = new LocacaoModel(
                        Convert.ToInt32(reader["Id"].ToString()),
                        Convert.ToInt32(reader["Id_Cliente"].ToString()),
                        Convert.ToInt32(reader["Id_Filme"].ToString()),
                        Convert.ToDateTime(reader["DataLocacao"].ToString())
                        );

                    try
                    {
                        locacao.dataDevolucao = Convert.ToDateTime(reader["DataDevolucao"].ToString());
                    }
                    catch { }

                    return Ok(locacao);
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
        /// Cadastra uma locacao
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpPost("Cadastrar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Cadastrar([FromBody] Locacao value)
        {
            using (MySqlConnection con = new MySqlConnection(Configuration["MysqlPath"]))
            {
                try
                {
                    con.Open();

                    MySqlCommand cmd = new MySqlCommand("INSERT INTO `locacao` (`Id_Cliente`,`Id_Filme`,`DataLocacao`) VALUES (@Id_Cliente, @Id_Filme, @DataLocacao);");
                    cmd.Parameters.Add("@Id_Cliente", MySqlDbType.Int32);
                    cmd.Parameters.Add("@Id_Filme", MySqlDbType.Int32);
                    cmd.Parameters.Add("@DataLocacao", MySqlDbType.DateTime);
                    cmd.Parameters["@Id_Cliente"].Value = value.idCliente;
                    cmd.Parameters["@Id_Filme"].Value = value.idFilme;
                    cmd.Parameters["@DataLocacao"].Value = value.DataLocacao;
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
        /// Atualiza uma locacao pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("Atualizar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Atualizar(int id, [FromBody] LocacaoAtualiza value)
        {
            using (MySqlConnection con = new MySqlConnection(Configuration["MysqlPath"]))
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE `locacao` SET `Id_Cliente` = @Id_Cliente,`Id_Filme` = @Id_Filme, `DataLocacao` = @DataLocacao, `DataDevolucao` = @DataDevolucao WHERE `Id` = @Id;");
                    cmd.Parameters.Add("@Id_Cliente", MySqlDbType.Int32);
                    cmd.Parameters.Add("@Id_Filme", MySqlDbType.Int32);
                    cmd.Parameters.Add("@DataLocacao", MySqlDbType.DateTime);
                    cmd.Parameters.Add("@DataDevolucao", MySqlDbType.DateTime);
                    cmd.Parameters.Add("@Id", MySqlDbType.Int32);
                    cmd.Parameters["@Id_Cliente"].Value = value.idCliente;
                    cmd.Parameters["@Id_Filme"].Value = value.idFilme;
                    cmd.Parameters["@DataLocacao"].Value = value.DataLocacao;
                    cmd.Parameters["@DataDevolucao"].Value = value.DataDevolucao;
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
        /// Deleta um locacao pelo id
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
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM `locacao` WHERE Id = @Id;");
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
