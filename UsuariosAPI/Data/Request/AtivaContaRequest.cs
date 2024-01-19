using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Data.Request
{
    public class AtivaContaRequest
    {
        [Required]
        public  string CodigoAtivacao { get; set; }
        public int UsuarioId { get; set; }
    }
}
