using AutoMapper;
using FilmesApi.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FluentResults;

public class EnderecoService
{
    private AppDbContext _context;
    private IMapper _mapper;

    public EnderecoService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Endereco AdicionaEndereco(CreateEnderecoDto enderecoDto)
    {
        Endereco endereco = _mapper.Map<Endereco>(enderecoDto);
        _context.Enderecos.Add(endereco);
        _context.SaveChanges();
        return endereco;
    }

    public IEnumerable<Endereco>RecuperaEnderecos()
    {
        return _context.Enderecos; 
    }

    public ReadEnderecoDto? RecuperaEnderecosPorId(int id)
    {
         Endereco? Endereco = _context.Enderecos.FirstOrDefault(Endereco => Endereco.Id == id);
            if(Endereco != null)
            {
                ReadEnderecoDto EnderecoDto = _mapper.Map<ReadEnderecoDto>(Endereco);
                return EnderecoDto;
            }
            return null;
    }

    public Result AtualizaEndereco(int id, UpdateEnderecoDto EnderecoDto)
    {
        Endereco? Endereco = _context.Enderecos.FirstOrDefault(Endereco => Endereco.Id == id);
            if(Endereco == null)
            {
                return Result.Fail("Endereço não encontrado");
            }
            _mapper.Map(EnderecoDto, Endereco);
            _context.SaveChanges();
            return Result.Ok();
    }

    public Result DeletaEndereco(int id)
    {
        Endereco? Endereco = _context.Enderecos.FirstOrDefault(Endereco => Endereco.Id == id);
            if (Endereco == null)
            {
                return Result.Fail("Endereço não encontrado");
            }
            _context.Remove(Endereco);
            _context.SaveChanges();
            return Result.Ok();
    }


}