using Locadora_WebAPI_DotNet.Model;
using Locadora_WebAPI_DotNet.Objeto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Locadora_WebAPI_DotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        private readonly IConfiguration Configuration;

        public FilmeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Lista todos os filmes cadastrados
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("Consultar")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FilmeModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Consultar()
        {
            using (MySqlConnection con = new MySqlConnection(Configuration["MysqlPath"]))
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM `filme`;");
                    cmd.Connection = con;

                    MySqlDataReader reader = cmd.ExecuteReader();

                    var lista = new List<FilmeModel>();

                    while (reader.Read())
                    {
                        var filme = new FilmeModel(
                            Convert.ToInt32(reader["Id"].ToString()),
                            reader["Titulo"].ToString(),
                            Convert.ToInt32(reader["ClassificacaoIndicativa"].ToString()),
                            Convert.ToInt32(reader["Lancamento"].ToString())
                            );

                        lista.Add(filme);
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
        /// Consulta um filme pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Consultar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FilmeModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Consultar(int id)
        {
            using (MySqlConnection con = new MySqlConnection(Configuration["MysqlPath"]))
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM `filme` WHERE Id = @Id;");
                    cmd.Parameters.Add("@Id", MySqlDbType.Int32);
                    cmd.Parameters["@Id"].Value = id;
                    cmd.Connection = con;

                    MySqlDataReader reader = cmd.ExecuteReader();

                    var filme = new FilmeModel(
                            Convert.ToInt32(reader["Id"].ToString()),
                            reader["Titulo"].ToString(),
                            Convert.ToInt32(reader["ClassificaoIndicativa"].ToString()),
                            Convert.ToInt32(reader["Lancamento"].ToString())
                            );

                    return Ok(filme);
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
        /// Cadastra um filme
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpPost("Cadastrar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Cadastrar([FromBody] Filme value)
        {
            using (MySqlConnection con = new MySqlConnection(Configuration["MysqlPath"]))
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO `filme` (`Titulo`,`ClassificacaoIndicativa`,`Lancamento`) VALUES ( @Titulo, @ClassificacaoIndicativa, @Lancamento);");
                    cmd.Parameters.Add("@Titulo", MySqlDbType.VarChar);
                    cmd.Parameters.Add("@ClassificacaoIndicativa", MySqlDbType.Int32);
                    cmd.Parameters.Add("@Lancamento", MySqlDbType.Int32);
                    cmd.Parameters["@Titulo"].Value = value.Titulo;
                    cmd.Parameters["@ClassificacaoIndicativa"].Value = value.ClassificacaoIndicativa;
                    cmd.Parameters["@Lancamento"].Value = value.Lancamento;
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
        /// Atualiza um filme pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("Atualizar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Atualizar(int id, [FromBody] Filme value)
        {
            using (MySqlConnection con = new MySqlConnection(Configuration["MysqlPath"]))
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE `filme` SET `Titulo` = @Titulo, `ClassificacaoIndicativa` = @ClassificacaoIndicativa, `Lancamento` = @Lancamento WHERE `Id` = @Id;");
                    cmd.Parameters.Add("@Titulo", MySqlDbType.VarChar);
                    cmd.Parameters.Add("@ClassificacaoIndicativa", MySqlDbType.Int32);
                    cmd.Parameters.Add("@Lancamento", MySqlDbType.Int32);
                    cmd.Parameters.Add("@Id", MySqlDbType.Int32);
                    cmd.Parameters["@Titulo"].Value = value.Titulo;
                    cmd.Parameters["@ClassificacaoIndicativa"].Value = value.ClassificacaoIndicativa;
                    cmd.Parameters["@Lancamento"].Value = value.Lancamento;
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
        /// Deleta um filme pelo id
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
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM `filme` WHERE Id = @Id;");
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
