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
            builder.Entity<EnderecoModel>()
                .HasOne(endereco => endereco.Cinema)
                .WithOne(cinema => cinema.Endereco)
                .HasForeignKey<CinemaModel>(cinema => cinema.EnderecoId);

            builder.Entity<CinemaModel>()
                .HasOne(gerente => gerente.Gerente)
                .WithMany(cinemas => cinemas.Cinemas)
                .HasForeignKey(cinema => cinema.GerenteId).OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<FilmeModel> Filmes { get; set; }
        public virtual DbSet<CinemaModel> Cinemas { get; set; }
        public DbSet<EnderecoModel> Enderecos { get; set; }

        public DbSet<GerenteModel> Gerentes { get; set; }
    }
}