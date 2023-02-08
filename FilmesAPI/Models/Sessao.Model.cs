using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models
{
    public class SessaoModel
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public virtual CinemaModel Cinema { get; set; }

        public virtual FilmeModel Filme { get; set; }

        public int FilmeID { get; set; }

        public int CinemaId { get; set; }

        public DateTime HorarioDeEncerramento { get; set; }
    }
}
