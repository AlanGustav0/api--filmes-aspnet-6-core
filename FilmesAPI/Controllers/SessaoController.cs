using AutoMapper;
using FilmesApi.Data;
using FilmesAPI.Data.Dtos.Sessao;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessaoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public SessaoController(AppDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionarSessao([FromBody] CreateSessaoDto sessaoDto) {

            Sessao sessao = _mapper.Map<Sessao>(sessaoDto);
            _context.Sessoes.Add(sessao);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperarSessoesPorId), new { sessao.Id }, sessao);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarSessoesPorId(int id)
        {
            Sessao sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.Id == id);
            if (sessao != null)
            {
                ReadSessaoDto readSessao = _mapper.Map<ReadSessaoDto>(sessao);
                return Ok(readSessao);
            };

            return NotFound();
        }
    }
}
