using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FilmesAPI.Models
{
    public class EnderecoModel
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string Logradouro { get; set; }

        public string Bairro { get; set; }

        public string Numero { get; set; }

        [JsonIgnore]
        public virtual CinemaModel Cinema { get; set; }
    }
}
