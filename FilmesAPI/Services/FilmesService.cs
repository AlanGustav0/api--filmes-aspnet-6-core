﻿using AutoMapper;
using FilmesApi.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FluentResults;

namespace FilmesAPI.Services
{
    public class FilmesService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public FilmesService(AppDbContext appContext, IMapper mapper) {
            _context = appContext;
            _mapper = mapper;
        }

        public FilmeModel AdicionaFilme(CreateFilmeDto filmeDto)
        {
            FilmeModel filme = _mapper.Map<FilmeModel>(filmeDto);

            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return filme;

        }
        public List<ReadFilmeDto>? RecuperaFilmePorFaixaEtaria(int? classificacaoEtaria)
        {
            List<FilmeModel> filmes;
            if (classificacaoEtaria == null)
            {
                filmes = _context.Filmes.ToList();
                return _mapper.Map<List<ReadFilmeDto>>(filmes);
            }
            filmes = _context.Filmes.Where(filme => filme.ClassificacaoEtaria <= classificacaoEtaria).ToList();

            if (filmes != null)
            {
                List<ReadFilmeDto> filmesDto = _mapper.Map<List<ReadFilmeDto>>(filmes);
                return filmesDto;
            }
            return null;
        }

        public ReadFilmeDto? RecuperaFilmePorId(int id)
        {
            FilmeModel? filme = RecuperaFilme(id);
            if(filme != null)
            {
                ReadFilmeDto readFilme = _mapper.Map<ReadFilmeDto>(filme);
                return readFilme;
            };

            return null;
        }

        public Result AtualizaFilme(int id,UpdateEnderecoDto filmeDto)
        {
            FilmeModel? filme = RecuperaFilme(id);
            if(filme == null)
            {
                return Result.Fail("Filme não encontrado");
            }
            return Result.Ok();
        }

        public Result DeleteFilmePorId(int id)
        {
            FilmeModel? filme = RecuperaFilme(id);
            if (filme == null)
            {
                return Result.Fail("Filme não encontrado");
            }
            _context.Remove(filme);
            _context.SaveChanges();
            return Result.Ok();
        }

        private FilmeModel? RecuperaFilme(int id)
        {
            FilmeModel? filme = _context.Filmes?.FirstOrDefault(filme => filme.Id == id);
            return filme;
        } 
    }
}
