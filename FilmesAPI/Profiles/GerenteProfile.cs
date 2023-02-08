using AutoMapper;
using FilmesAPI.Data.Dtos.Gerente;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class GerenteProfile : Profile
    {
        public GerenteProfile() {

            CreateMap<CreateGerenteDto, GerenteModel>();
            CreateMap<GerenteModel, ReadGerenteDto>()
                .ForMember(gerente => gerente.Cinemas, opts => opts
                .MapFrom(gerente => gerente.Cinemas.Select
                (c => new { c.Id, c.Nome, c.Endereco, c.EnderecoId})));
        }
    }
}
