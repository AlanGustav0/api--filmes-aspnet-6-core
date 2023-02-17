using AutoMapper;
using FilmesAPI.Data.Dtos.Sessao;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class SessaoProfile : Profile
    {
        public SessaoProfile() 
        {
            CreateMap<CreateSessaoDto,SessaoModel>();
            CreateMap<SessaoModel, ReadSessaoDto>()
                .ForMember(dto => dto.HorarioDeInicio, options => options
                .MapFrom(dto => dto.HorarioDeEncerramento.AddMinutes(dto.Filme.Duracao * (-1))));
        }
    }
}
