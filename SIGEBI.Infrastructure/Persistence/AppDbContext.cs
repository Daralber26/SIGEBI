using Microsoft.EntityFrameworkCore;
using SIGEBI.Domain.Entities;

namespace SIGEBI.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Recurso> Recursos => Set<Recurso>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Prestamo> Prestamos => Set<Prestamo>();

    public DbSet<Ejemplar> Ejemplares { get; set; }

}
