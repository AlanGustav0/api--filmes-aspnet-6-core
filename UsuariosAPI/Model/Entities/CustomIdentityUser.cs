using Microsoft.AspNetCore.Identity;

namespace UsuariosAPI.Model.Entities
{
    public class CustomIdentityUser : IdentityUser<int>
    {
        public DateTime DataNascimento { get; set; }
    }
}
