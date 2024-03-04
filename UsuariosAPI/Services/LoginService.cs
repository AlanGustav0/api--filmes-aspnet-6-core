
using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Data.Request;
using UsuariosAPI.Model;
using UsuariosAPI.Model.Entities;

namespace UsuariosAPI.Services
{
    public class LoginService
    {
        private SignInManager<CustomIdentityUser> _signInManager;
        private TokenService _tokenService;

        public LoginService(SignInManager<CustomIdentityUser> signInManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public Result LogaUsuario(LoginRequest loginRequest)
        {
            var resultadoIdentity = _signInManager.PasswordSignInAsync(loginRequest.Username, loginRequest.Password, false, false).Result;
             if (resultadoIdentity.Succeeded)
             {
                var identityUser = _signInManager.UserManager.Users.FirstOrDefault(usuario => 
                    usuario.NormalizedUserName == loginRequest.Username.ToUpper());
                Token token = _tokenService.CreateToken(identityUser,
                    _signInManager.UserManager.GetRolesAsync(identityUser).Result.FirstOrDefault());

                return Result.Ok().WithSuccess(token.Valor);
             }
                
            return Result.Fail("Login falhou");
        }

        public Result SolicitaResetSenhaUsuario(SolicitaResetRequest solicitaResetRequest)
        {
            CustomIdentityUser identityUser = RecuperaUsuarioPorEmail(solicitaResetRequest.Email);

            if (!string.IsNullOrEmpty(identityUser.Email))
            {
                string codigoRecuperacao = _signInManager.UserManager.GeneratePasswordResetTokenAsync(identityUser).Result;

                return Result.Ok().WithSuccess(codigoRecuperacao);
            }

            return Result.Fail("Falha ao solicitar redefinição");
        }


        public Result ResetSenhaUsuario(EfetuaResetRequest efetuaResetRequest)
        {
            CustomIdentityUser identityUser = RecuperaUsuarioPorEmail(efetuaResetRequest.Email);

            IdentityResult identityResult = _signInManager.UserManager.ResetPasswordAsync(identityUser, efetuaResetRequest.Token, efetuaResetRequest.Password).Result;

            if (identityResult.Succeeded) return Result.Ok().WithSuccess("Senha redefinida com sucesso");
            
            return Result.Fail("Falha na redefinição de senha");
        }

        private CustomIdentityUser RecuperaUsuarioPorEmail(string email)
        {
            return _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedEmail == email.ToUpper());
        }
    }
}
