using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Model
{
    public class SolicitaResetRequest
    {
        [Required]
        public string Email { get; set; }
    }
}