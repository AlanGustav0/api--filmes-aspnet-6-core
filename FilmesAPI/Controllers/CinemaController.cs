using AutoMapper;
using FilmesApi.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;



        public CinemaController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
  

        [HttpPost]
        public IActionResult AdicionaCinema([FromBody] CreateCinemaDto cinemaDto)
        {
            CinemaModel cinema = _mapper.Map<CinemaModel>(cinemaDto);
            _context.Cinemas.Add(cinema);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaCinemasPorId), new { cinema.Id }, cinema);
        }


        [HttpGet("{id}")]
        public IActionResult RecuperaCinemasPorId(int id)
        {
            CinemaModel cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if(cinema != null)
            {
                ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
                return Ok(cinemaDto);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult RecuperaCinemas([FromQuery] string? nomeDoFilme)
        {
            List<CinemaModel> cinemas = _context.Cinemas.ToList();
            if(cinemas == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(nomeDoFilme))
            {
                IEnumerable<CinemaModel> query = from cinema in cinemas 
                            where cinema.Sessoes.Any(sessao => 
                            sessao.Filme.Titulo == nomeDoFilme)
                            select cinema;
                cinemas = query.ToList();
            }
            List<ReadCinemaDto> cinemaDto = _mapper.Map<List<ReadCinemaDto>>(cinemas);
            return Ok(cinemaDto);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
        {
            CinemaModel cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if(cinema == null)
            {
                return NotFound();
            }
            _mapper.Map(cinemaDto, cinema);
            _context.SaveChanges();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeletaCinema(int id)
        {
            CinemaModel cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
            {
                return NotFound();
            }
            _context.Remove(cinema);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
