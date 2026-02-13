using Microsoft.EntityFrameworkCore;
using SIGEBI.Application.Abstractions;
using SIGEBI.Application.UseCases.Auth;
using SIGEBI.Application.UseCases.Catalogo;
using SIGEBI.Application.UseCases.Prestamos;
using SIGEBI.Application.UseCases.Recursos;
using SIGEBI.Infrastructure.Persistence;
using SIGEBI.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// EF Core + SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositorios (EF)
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepositoryEf>();
builder.Services.AddScoped<IRecursoRepository, RecursoRepositoryEf>();
builder.Services.AddScoped<IPrestamoRepository, PrestamoRepositoryEf>();

// Casos de uso
builder.Services.AddScoped<ListarCatalogo>();
builder.Services.AddScoped<LoginUsuario>();

builder.Services.AddScoped<CrearRecurso>();
builder.Services.AddScoped<ActualizarRecurso>();
builder.Services.AddScoped<EliminarRecurso>();

builder.Services.AddScoped<CrearPrestamo>();

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