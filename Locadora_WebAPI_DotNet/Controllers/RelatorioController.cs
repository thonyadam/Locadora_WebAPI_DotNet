using Locadora_WebAPI_DotNet.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Locadora_WebAPI_DotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatorioController : ControllerBase
    {
        readonly IConfiguration Configuration;

        public RelatorioController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Lista os clientes em atraso na devolução
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("ClientesDevendo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClienteModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ClientesDevendo()
        {
            using (MySqlConnection con = new MySqlConnection(Configuration["MysqlPath"]))
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(
                        "select c.* " +
                        "from locacao as l " +
                        "left join filme f on f.Id = l.Id_Filme " +
                        "left join cliente c on c.Id = l.Id_Cliente " +
                        "where l.DataDevolucao is null " +
                        "and ( (f.Lancamento <> 0 and datediff(curdate(),l.DataLocacao) > 2) " +
                        "	or (f.Lancamento = 0 and datediff(curdate(),l.DataLocacao) > 3) ); ");
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
        /// Lista os filmes que nunca foram alugados
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("FilmesNaoAlugados")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FilmeModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult FilmesNaoAlugados()
        {
            using (MySqlConnection con = new MySqlConnection(Configuration["MysqlPath"]))
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(
                        "select f.* " +
                        "from filme f " +
                        "left join locacao l on l.Id_Filme = f.Id " +
                        "where l.Id_Filme is null; ");
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
        /// Lista os cinco filmes mais alugados do último ano
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("Top5Filmes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FilmeModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Top5Filmes()
        {
            using (MySqlConnection con = new MySqlConnection(Configuration["MysqlPath"]))
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(
                        "select f.Id, f.Titulo, f.ClassificacaoIndicativa, f.Lancamento , count(*) contador " +
                        "from locacao as l " +
                        "left join filme f on f.Id = l.Id_Filme " +
                        "where year(l.DataLocacao) = year(curdate()) " +
                        "group by f.Id, f.Titulo " +
                        "order by contador desc " +
                        "limit 5; ");
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
        /// Lista os três filmes menos alugados da última semana
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("Piores3Filmes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FilmeModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Piores3Filmes()
        {
            using (MySqlConnection con = new MySqlConnection(Configuration["MysqlPath"]))
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(
                        "select f.Id, f.Titulo, f.ClassificacaoIndicativa, f.Lancamento , sum(case when l.Id_Filme is not null then 1 else 0 end) contador " +
                        "from filme f  " +
                        "left join locacao l on l.Id_Filme = f.Id " +
                        "where l.DataLocacao > curdate() - 7 " +
                        "group by f.Id, f.Titulo " +
                        "order by contador asc " +
                        "limit 3; ");
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
        /// Lista O segundo cliente que mais alugou filmes
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("Top2Cliente")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClienteModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Top2Cliente()
        {
            using (MySqlConnection con = new MySqlConnection(Configuration["MysqlPath"]))
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(
                        "select c.Id, c.Nome, c.CPF, c.DataNascimento, sum(case when l.Id_Cliente is not null then 1 else 0 end) contador " +
                        "from cliente c " +
                        "left join locacao l on l.Id_Cliente = c.Id " +
                        "group by c.Id, c.Nome, c.CPF, C.DataNascimento " +
                        "order by contador desc " +
                        "limit 2, 1; ");
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
    }
}
