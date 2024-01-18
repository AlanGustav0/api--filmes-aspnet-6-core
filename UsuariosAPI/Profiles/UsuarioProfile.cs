using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Data.Dto;

namespace UsuariosAPI.UsuarioProfile;
public class UsuarioProfile: Profile
{
    public UsuarioProfile()
    {
        CreateMap<CreateUsuarioDto,Usuario>();
        CreateMap<Usuario,IdentityUser<int>>();
    }
}