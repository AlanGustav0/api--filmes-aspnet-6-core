using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private static List<Filme> filmes = new List<Filme>();
        private static int id = 1;

        [HttpPost]
        public IActionResult AdicionaFilme([FromBody]Filme filme)
        {
            filme.Id = id++;
            filmes.Add(filme);
            Console.WriteLine(filme.Titulo);
            return CreatedAtAction(nameof(RecuperaFilmePorId),new {id = filme.Id},filme);
        }

        [HttpGet]
        public IEnumerable<Filme> RecuperaFilmes()
        {
            return filmes;
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaFilmePorId(int id)
        {

            Filme filme = filmes.FirstOrDefault(filme => filme.Id == id);

            return filme != null ? Ok(filme) : NotFound();

        }
    }
}
