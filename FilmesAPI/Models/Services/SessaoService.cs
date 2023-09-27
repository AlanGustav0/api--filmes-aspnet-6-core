using AutoMapper;
using FilmesApi.Data;
using FilmesAPI.Data.Dtos.Sessao;
using FilmesAPI.Models;

public class SessaoService
{
    private AppDbContext _context;
    private IMapper _mapper;

    public SessaoService(IMapper mapper, AppDbContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public Sessao AdicionarSessao(CreateSessaoDto sessaoDto)
    {
        Sessao sessao = _mapper.Map<Sessao>(sessaoDto);
        _context.Sessoes.Add(sessao);
        _context.SaveChanges();
        return sessao;
    }

    public ReadSessaoDto? RecuperarSessoesPorId(int id)
    {
        Sessao? sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.Id == id);
        if (sessao != null)
        {
            ReadSessaoDto readSessao = _mapper.Map<ReadSessaoDto>(sessao);
            return readSessao;
        };

        return null;
    }
}