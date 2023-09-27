using AutoMapper;
using FilmesApi.Data;
using FilmesAPI.Data.Dtos.Gerente;
using FilmesAPI.Models;
using FluentResults;

public class GerenteService
{
    private AppDbContext _context;
    private IMapper _mapper;

    public GerenteService(IMapper mapper, AppDbContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public Gerente AdicionaGerente(CreateGerenteDto gerenteDto)
    {
        Gerente gerente = _mapper.Map<Gerente>(gerenteDto);

        _context.Gerentes.Add(gerente);
        _context.SaveChanges();
        return gerente;
    }

    public List<Gerente> RecuperaGerentes()
    {
        return _context.Gerentes.ToList();
    }

    public ReadGerenteDto? RecuperaGerentePorId(int id)
    {
        Gerente? Gerente = RecuperaGerente(id);
        if (Gerente != null)
        {
            ReadGerenteDto readGerente = _mapper.Map<ReadGerenteDto>(Gerente);
            return readGerente;
        };

        return null;
    }

    public Result DeletaGerente(int id)
    {
        Gerente? gerente = RecuperaGerente(id);
        if (gerente == null)
        {
            return Result.Fail("Gerente não encontrado");
        }

        return Result.Ok();
    }

    private Gerente? RecuperaGerente(int id)
    {
        Gerente? gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);
        return gerente;
    }
}