using AutoMapper;
using FilmesApi.Data;
using FilmesAPI.Controllers;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FilmesAPI.Profiles;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FilmesAPI.Tests
{
    
    public class CinemaControllerTest
    {
      
        [Fact]
        public void ShouldCreateCinema()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "FilmeConnection")
                .Options;

            
            var mockSet = new Mock<DbSet<CinemaModel>>();

            var mockContext = new Mock<AppDbContext>(options);
            mockContext.Setup(m => m.Cinemas).Returns(mockSet.Object);

            var myProfile = new CinemaProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mockMapper = new Mapper(configuration);

            var controller = new CinemaController(mockContext.Object, mockMapper);
            var cinema = controller.AdicionaCinema(new CreateCinemaDto() { Nome = "Cinema", EnderecoId = 2 });

            Assert.NotNull(cinema);
            mockSet.Verify(m => m.Add(It.IsAny<CinemaModel>()),Times.Once());
            mockContext.Verify(m => m.SaveChanges(),Times.Once());
        }
    }
}