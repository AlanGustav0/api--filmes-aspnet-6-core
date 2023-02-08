using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FilmesAPI.Models
{
    public class CinemaModel
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo de nome é obrigatório")]
        public string Nome { get; set; }
            
        public virtual EnderecoModel Endereco { get; set; }

        public int EnderecoId { get; set; }

        public virtual GerenteModel Gerente { get; set; }

        public int GerenteId { get; set; }
    }
}