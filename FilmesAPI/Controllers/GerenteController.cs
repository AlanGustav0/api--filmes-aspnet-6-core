using AutoMapper;
using FilmesApi.Data;
using FilmesAPI.Data.Dtos.Gerente;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;


namespace GerentesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GerenteController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public GerenteController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionaGerente([FromBody] CreateGerenteDto gerenteDto)
        {
            Gerente gerente = _mapper.Map<Gerente>(gerenteDto);
            
            _context.Gerentes.Add(gerente);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaGerentePorId),new {id = gerente.Id}, gerente);
        }

        [HttpGet]
        public IActionResult RecuperaGerentes()
        {
            return Ok(_context.Gerentes.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaGerentePorId(int id)
        {

            Gerente Gerente = RecuperaGerente(id);
            if(Gerente != null)
            {
                ReadGerenteDto readGerente = _mapper.Map<ReadGerenteDto>(Gerente);
                return Ok(readGerente);
            };

            return NotFound();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGerentePorId(int id)
        {
            Gerente gerente = RecuperaGerente(id);
            if (gerente == null)
            {
                return NotFound();
            }
            _context.Remove(gerente);
            _context.SaveChanges();
            return NoContent();
        }

        private Gerente RecuperaGerente(int id)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);
            return gerente;
        }
    }
}
