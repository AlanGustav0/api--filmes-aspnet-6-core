using FilmesAPI.Models;

namespace FilmesAPI.Data.Dtos.Gerente
{
    public class ReadGerenteDto
    {
        public int id { get; set; }
        public string Nome { get; set; }

        public Object Cinemas { get; set; }
    }
}
