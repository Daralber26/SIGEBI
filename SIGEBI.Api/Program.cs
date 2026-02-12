using Microsoft.EntityFrameworkCore;
using SIGEBI.Application.Abstractions;
using SIGEBI.Application.UseCases.Auth;
using SIGEBI.Application.UseCases.Catalogo;
using SIGEBI.Infrastructure.Persistence;
using SIGEBI.Infrastructure.Repositories;
using SIGEBI.Application.UseCases.Recursos;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// EF Core + SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositorios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepositoryEf>();
builder.Services.AddScoped<IRecursoRepository, RecursoRepositoryEf>();

// Casos de uso
builder.Services.AddScoped<ListarCatalogo>();
builder.Services.AddScoped<LoginUsuario>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
