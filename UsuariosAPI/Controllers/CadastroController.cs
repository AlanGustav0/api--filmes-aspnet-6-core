
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Data.Dto;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
        private CadastroService _cadastroService;

        public CadastroController(CadastroService cadastroService)
        {
            _cadastroService = cadastroService;
        }

        [HttpPost]
        public IActionResult CadastroUsuario(CreateUsuarioDto createUsuarioDto)
        {
            Result resultado = _cadastroService.CadastroUsuario(createUsuarioDto);

            if (resultado.IsFailed) return StatusCode(500);
            return Ok();
        }

    }
}