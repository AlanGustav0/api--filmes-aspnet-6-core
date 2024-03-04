using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FilmesAPI.Authorization
{
    public class IdadeMinimaHandler : AuthorizationHandler<IdadeMinimaRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdadeMinimaRequirement requirement)
        {
            if(!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth)) return Task.CompletedTask;

            DateTime dataNascimento = Convert.ToDateTime(context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth).Value);

            int idadeObtidade = DateTime.Today.Year - dataNascimento.Year;

            if (dataNascimento > DateTime.Today.AddYears(-idadeObtidade)) idadeObtidade--;

            if (idadeObtidade >= requirement.IdadeMinima) context.Succeed(requirement); 
            
            return Task.CompletedTask;
        }
    }
}
