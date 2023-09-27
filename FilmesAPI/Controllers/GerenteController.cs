using FilmesAPI.Data.Dtos.Gerente;
using FilmesAPI.Models;
using FluentResults;
using Microsoft.AspNetCore.Mvc;


namespace GerentesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GerenteController : ControllerBase
    {
        private GerenteService _gerenteService;

        public GerenteController(GerenteService gerenteService)
        {
            _gerenteService = gerenteService;

        }

        [HttpPost]
        public IActionResult AdicionaGerente([FromBody] CreateGerenteDto gerenteDto)
        {
            Gerente gerente = _gerenteService.AdicionaGerente(gerenteDto);

            return CreatedAtAction(nameof(RecuperaGerentePorId), new { id = gerente.Id }, gerente);
        }

        [HttpGet]
        public IActionResult RecuperaGerentes()
        {
            return Ok(_gerenteService.RecuperaGerentes());
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaGerentePorId(int id)
        {

            ReadGerenteDto? gerenteDto = _gerenteService.RecuperaGerentePorId(id);
            if (gerenteDto != null)
            {

                return Ok(gerenteDto);
            };

            return NotFound();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGerentePorId(int id)
        {
            Result result = _gerenteService.DeletaGerente(id);
            if (result.IsFailed)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
