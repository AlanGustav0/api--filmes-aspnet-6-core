using FilmesAPI.Models;

namespace FilmesAPI.Data.Dtos.Sessao
{
    public class ReadSessaoDto
    {   
        public int Id { get; set; }
        public CinemaModel Cinema { get; set; }
        public FilmeModel Filme { get; set; }

        public DateTime HorarioDeEncerramento { get; set; }

        public DateTime HorarioDeInicio { get; set; }

    }
}
