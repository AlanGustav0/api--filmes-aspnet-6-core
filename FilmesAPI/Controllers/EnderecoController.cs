using AutoMapper;
using FilmesApi.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public EnderecoController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
  

        [HttpPost]
        public IActionResult AdicionaEndereco([FromBody] CreateEnderecoDto enderecoDto)
        {
            EnderecoModel endereco = _mapper.Map<EnderecoModel>(enderecoDto);
            _context.Enderecos.Add(endereco);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaEnderecosPorId), new { Id = endereco.Id }, endereco);
        }

        [HttpGet]
        public IEnumerable<EnderecoModel> RecuperaEnderecos()
        {
            return _context.Enderecos;
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaEnderecosPorId(int id)
        {
            EnderecoModel Endereco = _context.Enderecos.FirstOrDefault(Endereco => Endereco.Id == id);
            if(Endereco != null)
            {
                ReadEnderecoDto EnderecoDto = _mapper.Map<ReadEnderecoDto>(Endereco);
                return Ok(EnderecoDto);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaEndereco(int id, [FromBody] UpdateEnderecoDto EnderecoDto)
        {
            EnderecoModel Endereco = _context.Enderecos.FirstOrDefault(Endereco => Endereco.Id == id);
            if(Endereco == null)
            {
                return NotFound();
            }
            _mapper.Map(EnderecoDto, Endereco);
            _context.SaveChanges();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeletaEndereco(int id)
        {
            EnderecoModel Endereco = _context.Enderecos.FirstOrDefault(Endereco => Endereco.Id == id);
            if (Endereco == null)
            {
                return NotFound();
            }
            _context.Remove(Endereco);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
