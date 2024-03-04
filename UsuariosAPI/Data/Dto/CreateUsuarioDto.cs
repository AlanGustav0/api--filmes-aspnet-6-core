using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Data.Dto
{
    public class CreateUsuarioDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string RePassword { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }
    }
}
