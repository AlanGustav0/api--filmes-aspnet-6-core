using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Data.Request;
using UsuariosAPI.Model;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult LogaUsuario(LoginRequest loginRequest)
        {
            Result resultado = _loginService.LogaUsuario(loginRequest);

            if(resultado.IsFailed) return Unauthorized(resultado.Errors.FirstOrDefault());
            return Ok(resultado.Successes.FirstOrDefault());

        }

        [HttpPost("/solicita-reset")]
        public IActionResult SolicitaResetSenhaUsuario(SolicitaResetRequest solicitaResetRequest)
        {
            Result resultado = _loginService.SolicitaResetSenhaUsuario(solicitaResetRequest);
            if(resultado.IsFailed) return Unauthorized(resultado.Errors.FirstOrDefault());

            return Ok(resultado.Successes.FirstOrDefault());

        }
    }
}
