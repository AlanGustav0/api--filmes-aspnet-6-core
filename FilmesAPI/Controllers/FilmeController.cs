using AutoMapper;
using FilmesApi.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;


namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public FilmeController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            FilmeModel filme = _mapper.Map<FilmeModel>(filmeDto);
            
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaFilmePorId),new {id = filme.Id},filme);
        }

        [HttpGet]
        public IEnumerable<FilmeModel> RecuperaFilmes()
        {
            return _context.Filmes;
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaFilmePorId(int id)
        {

            FilmeModel filme = RecuperaFilme(id);
            if(filme != null)
            {
                ReadFilmeDto readFilme = _mapper.Map<ReadFilmeDto>(filme);
                return Ok(readFilme);
            };

            return NotFound();

        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateEnderecoDto filmeDto)
        {
            FilmeModel filme = RecuperaFilme(id);
            if(filme == null)
            {
                return NotFound();
            }
            filme = _mapper.Map<FilmeModel>(filmeDto);

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFilmePorId(int id)
        {
            FilmeModel filme = RecuperaFilme(id);
            if (filme == null)
            {
                return NotFound();
            }
            _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }

        private FilmeModel RecuperaFilme(int id)
        {
            FilmeModel filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            return filme;
        }
    }
}
