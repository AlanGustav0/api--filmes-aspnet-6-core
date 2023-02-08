using FilmesAPI.Models;
using System.ComponentModel.DataAnnotations;


namespace FilmesAPI.Data.Dtos
{
    public class ReadCinemaDto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo de nome é obrigatório")]
        public string Nome { get; set; }
        public EnderecoModel Endereco { get; set; }

        public GerenteModel Gerente { get; set; }


    }
}
