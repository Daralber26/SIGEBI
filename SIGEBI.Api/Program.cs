using Microsoft.EntityFrameworkCore;
using SIGEBI.Application.Abstractions;
using SIGEBI.Application.UseCases.Auth;
using SIGEBI.Application.UseCases.Catalogo;
using SIGEBI.Application.UseCases.Ejemplares;
using SIGEBI.Application.UseCases.Prestamos;
using SIGEBI.Application.UseCases.Recursos;
using SIGEBI.Application.UseCases.Reservas;
using SIGEBI.Infrastructure.Persistence;
using SIGEBI.Infrastructure.Repositories;
using SIGEBI.Api; // IMPORTANTE (para UseApiExceptionHandling y UseApiAuditing)

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// EF Core + SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositorios (EF)
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepositoryEf>();
builder.Services.AddScoped<IRecursoRepository, RecursoRepositoryEf>();
builder.Services.AddScoped<IPrestamoRepository, PrestamoRepositoryEf>();
builder.Services.AddScoped<IEjemplarRepository, EjemplarRepositoryEf>();
builder.Services.AddScoped<IReservaRepository, ReservaRepositoryEf>();

// NUEVO: Catálogo (calcula copias disponibles)
builder.Services.AddScoped<ICatalogoRepository, CatalogoRepository>();

// Casos de uso
builder.Services.AddScoped<ListarCatalogo>();
builder.Services.AddScoped<LoginUsuario>();

builder.Services.AddScoped<CrearRecurso>();
builder.Services.AddScoped<ActualizarRecurso>();
builder.Services.AddScoped<EliminarRecurso>();

builder.Services.AddScoped<CrearPrestamo>();
builder.Services.AddScoped<CrearEjemplar>();
builder.Services.AddScoped<DevolverPrestamo>();

builder.Services.AddScoped<CrearReserva>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Middleware global de errores (tiene que ir ANTES de authorization y controllers)
app.UseApiExceptionHandling();

// Auditoría
app.UseApiAuditing();

app.UseAuthorization();

app.MapControllers();

app.Run();