using AutoMapper;
using UsuariosAPI.Data.Dto;

namespace UsuariosAPI.UsuarioProfile;
public class UsuarioProfile: Profile
{
    public UsuarioProfile()
    {
        CreateMap<CreateUsuarioDto,Usuario>();
    }
}