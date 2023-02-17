using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Relacionamento 1:1
            builder.Entity<EnderecoModel>()
                .HasOne(endereco => endereco.Cinema)
                .WithOne(cinema => cinema.Endereco)
                .HasForeignKey<CinemaModel>(cinema => cinema.EnderecoId);

            //Relacionamento 1:n
            builder.Entity<CinemaModel>()
                .HasOne(gerente => gerente.Gerente)
                .WithMany(cinemas => cinemas.Cinemas)
                .HasForeignKey(cinema => cinema.GerenteId);

            //Relacionamento n:n
            builder.Entity<SessaoModel>()
                .HasOne(sessao => sessao.Filme)
                .WithMany(filme => filme.Sessoes)
                .HasForeignKey(sessao => sessao.FilmeId);

            builder.Entity<SessaoModel>()
                .HasOne(sessao => sessao.Cinema)
                .WithMany(cinema => cinema.Sessoes)
                .HasForeignKey(sessao => sessao.CinemaId);
        }

        public DbSet<FilmeModel> Filmes { get; set; }
        public virtual DbSet<CinemaModel> Cinemas { get; set; }
        public DbSet<EnderecoModel> Enderecos { get; set; }

        public DbSet<GerenteModel> Gerentes { get; set; }

        public DbSet<SessaoModel> Sessoes { get; set; }
    }
}