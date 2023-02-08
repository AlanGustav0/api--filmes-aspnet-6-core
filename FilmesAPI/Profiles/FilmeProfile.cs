using AutoMapper;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using System.IO;

namespace FilmesAPI.Profiles
{
    public class FilmeProfile : Profile
    {
        public FilmeProfile() 
        {
            CreateMap<CreateFilmeDto, FilmeModel>();
            CreateMap<FilmeModel, ReadFilmeDto>();
            CreateMap<UpdateEnderecoDto, FilmeModel>();
        }
    }
}
