using AutoMapper;
using FilmesApi.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FluentResults;

namespace FilmesAPI.Services
{
    public class CinemaService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public CinemaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public CinemaModel AdicionaCinema(CreateCinemaDto cinemaDto)
        {
            CinemaModel cinema = _mapper.Map<CinemaModel>(cinemaDto);
            _context.Cinemas.Add(cinema);
            _context.SaveChanges();

            return cinema;

        }

        public ReadCinemaDto? RecuperaCinemasPorId(int id)
        {
            CinemaModel cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema != null)
            {
                ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
                return cinemaDto;
            }

            return null;
        }

        public List<ReadCinemaDto> RecuperaCinemas(string nomeDoFilme)
        {
            List<CinemaModel> cinemas = _context.Cinemas.ToList();
            if (cinemas == null)
            {
                return null;
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

            return cinemaDto;

        }

        public Result AtualizaCinema(int id, UpdateCinemaDto cinemaDto)
        {
            CinemaModel cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
            {
                return Result.Fail("Cinema não encontrado");
            }
            _mapper.Map(cinemaDto, cinema);
            _context.SaveChanges();

            return Result.Ok();
        }

        public Result DeleteCinema(int id)
        {
            CinemaModel cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
            {
                return Result.Fail("Cinema não encontrado para ser deletado");
            }
            _context.Remove(cinema);
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}
