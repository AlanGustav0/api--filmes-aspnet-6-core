using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FilmesAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private readonly CinemaService _cinemaService;
        public CinemaController(CinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }

        [HttpPost]
        public IActionResult AdicionaCinema([FromBody] CreateCinemaDto cinemaDto)
        {
            CinemaModel cinema = _cinemaService.AdicionaCinema(cinemaDto);

            return CreatedAtAction(nameof(RecuperaCinemasPorId), new { cinema.Id }, cinema);
        }


        [HttpGet("{id}")]
        public IActionResult RecuperaCinemasPorId(int id)
        {
            ReadCinemaDto? readCinemaDto = _cinemaService.RecuperaCinemasPorId(id);
            if(readCinemaDto != null)
            {
                return Ok(readCinemaDto);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult RecuperaCinemas([FromQuery] string nomeDoFilme)
        {
            List<ReadCinemaDto>? listCinemaDto = _cinemaService.RecuperaCinemas(nomeDoFilme);
            if(listCinemaDto == null)
            {
                return NotFound();
            }

            return Ok(listCinemaDto);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
        {
            Result result = _cinemaService.AtualizaCinema(id,cinemaDto);

            if(result.IsFailed) return NotFound();
            
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeletaCinema(int id)
        {
            Result result = _cinemaService.DeleteCinema(id);

            if (result.IsFailed) return NotFound();
            
            return NoContent();
        }

    }
}
