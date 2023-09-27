using FilmesAPI.Data.Dtos.Sessao;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessaoController : ControllerBase
    {
        private SessaoService _sessaoService;
        public SessaoController(SessaoService sessaoService) 
        {
            _sessaoService = sessaoService;
        }

        [HttpPost]
        public IActionResult AdicionarSessao([FromBody] CreateSessaoDto sessaoDto) {

            Sessao sessao = _sessaoService.AdicionarSessao(sessaoDto);
           
            return CreatedAtAction(nameof(RecuperarSessoesPorId), new { sessao.Id }, sessao);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarSessoesPorId(int id)
        {
            ReadSessaoDto? readSessaoDto = _sessaoService.RecuperarSessoesPorId(id);
            if (readSessaoDto != null)
            {
                return Ok(readSessaoDto);
            };

            return NotFound();
        }
    }
}
