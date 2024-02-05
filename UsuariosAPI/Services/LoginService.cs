
using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Data.Request;
using UsuariosAPI.Model;

namespace UsuariosAPI.Services
{
    public class LoginService
    {
        private SignInManager<IdentityUser<int>> _signInManager;
        private TokenService _tokenService;

        public LoginService(SignInManager<IdentityUser<int>> signInManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public Result LogaUsuario(LoginRequest loginRequest)
        {
            var resultadoIdentity = _signInManager.PasswordSignInAsync(loginRequest.Username,loginRequest.Password,false,false);
             if (resultadoIdentity.Result.Succeeded)
             {
                var identityUser = _signInManager.UserManager.Users.FirstOrDefault(usuario => 
                    usuario.NormalizedUserName == loginRequest.Username.ToUpper());
                Token token = _tokenService.CreateToken(identityUser);
                return Result.Ok().WithSuccess(token.Valor);
             }
                
            return Result.Fail("Login falhou");
        }

        public Result SolicitaResetSenhaUsuario(SolicitaResetRequest solicitaResetRequest)
        {
            IdentityUser<int> identityUser = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedEmail == solicitaResetRequest.Email.ToUpper());

            if(!string.IsNullOrEmpty(identityUser.Email))
            {
                string codigoRecuperacao = _signInManager.UserManager.GeneratePasswordResetTokenAsync(identityUser).Result;

                return Result.Ok().WithSuccess(codigoRecuperacao);
            }

            return Result.Fail("Falha ao solicitar redefinição");
        }
    }
}
