using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using MySql.EntityFrameworkCore.Extensions;
using UsuariosAPI.Model;
using UsuariosAPI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add service database connection with IdentityServer
builder.Services.AddDbContext<UserDbContext>(options => {
    options.UseMySQL(builder.Configuration.GetConnectionString("UsuarioConnection"));
    
    },ServiceLifetime.Transient);
builder.Services.AddIdentity<IdentityUser<int>,IdentityRole<int>>(
    opt => opt.SignIn.RequireConfirmedEmail = false
    )
    .AddEntityFrameworkStores<UserDbContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<CadastroService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<LogoutService>();
builder.Services.AddScoped<TokenService>();

/* Use this configuration to configure Local Secrets on Application

You will need to use the command line (dotnet user-secrets set) to set values in the secret
*/
builder.Host.ConfigureAppConfiguration((context, builder) => 
            builder.AddUserSecrets<Program>());


// If you want to configure identity password format options
/*
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 8;
});*/



// Automapper configuration services
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

internal class MyDesignTimeServices : IDesignTimeServices
{
    public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddEntityFrameworkMySQL();
        new EntityFrameworkRelationalDesignServicesBuilder(serviceCollection).TryAddCoreServices();
    }

};