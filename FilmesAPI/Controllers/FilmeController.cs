﻿using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FilmesAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private readonly FilmesService _filmeService;

        public FilmeController(FilmesService filmeService)
        {
            _filmeService = filmeService;
        }

        [HttpPost]
        [Authorize(Roles = "admin, regular")]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            Filme filme = _filmeService.AdicionaFilme(filmeDto);

            return CreatedAtAction(nameof(RecuperaFilmePorId), new { id = filme.Id }, filme);
        }



        [HttpGet]
        [Authorize(Roles = "admin, regular", Policy = "IdadeMinima")]
        public IActionResult RecuperaFilmePorFaixaEtaria([FromQuery] int? classificacaoEtaria = null)
        {

            List<ReadFilmeDto>? listFilmesDto = _filmeService.RecuperaFilmePorFaixaEtaria(classificacaoEtaria);
            if (listFilmesDto != null) return Ok(listFilmesDto);

            return NotFound();

        }

        [HttpGet("{id}")]
        public IActionResult RecuperaFilmePorId(int id)
        {

            ReadFilmeDto? readFilme = _filmeService.RecuperaFilmePorId(id);
            if (readFilme != null)
            {
                return Ok(readFilme);
            };

            return NotFound();

        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateEnderecoDto filmeDto)
        {
            Result result = _filmeService.AtualizaFilme(id, filmeDto);
            if (result.IsFailed) return NotFound();
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFilmePorId(int id)
        {
            Result result = _filmeService.DeleteFilmePorId(id);
            if (result == null) return NotFound();
            
            return NoContent();
        }

    }
}
