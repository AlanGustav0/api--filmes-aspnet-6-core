using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos
{
    public class ReadEnderecoDto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo Logradouro é obrigatório")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "O campo Bairro é obrigatório")]
        public string Bairro { get; set; }

        [StringLength(15, ErrorMessage = "O gênero não pode ter mais de 15 caracteres")]
        public string Numero { get; set; }
        public string HoraDaConsulta { get; set; }
    }
}
