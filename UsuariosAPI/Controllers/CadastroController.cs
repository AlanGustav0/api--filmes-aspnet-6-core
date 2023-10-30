
using Microsoft.AspNetCore.Mvc;

namespace UsuariosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CadastroController
    {

        [HttpPost]
        public IActionResult CadastroUsuario(CreateCadastroDto cadastroDto)
        {
            return null;
        }

    }
}