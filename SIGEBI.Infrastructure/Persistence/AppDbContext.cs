using Microsoft.EntityFrameworkCore;
using SIGEBI.Domain.Entities;

namespace SIGEBI.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Recurso> Recursos => Set<Recurso>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Prestamo> Prestamos => Set<Prestamo>();
    public DbSet<Ejemplar> Ejemplares => Set<Ejemplar>();

    public DbSet<Auditoria> Auditorias => Set<Auditoria>();
    public DbSet<Penalizacion> Penalizaciones => Set<Penalizacion>();
    public DbSet<Notificacion> Notificaciones => Set<Notificacion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Auditoria>(b =>
        {
            b.ToTable("Auditorias");
            b.HasKey(x => x.Id);

            b.Property(x => x.Metodo)
                .HasMaxLength(10)
                .IsRequired();

            b.Property(x => x.Ruta)
                .HasMaxLength(300)
                .IsRequired();

            b.Property(x => x.TraceId)
                .HasMaxLength(100)
                .IsRequired();

            b.Property(x => x.Ip)
                .HasMaxLength(45);

            b.Property(x => x.Usuario)
                .HasMaxLength(150);

            b.Property(x => x.Detalle)
                .HasMaxLength(300);
        });

        modelBuilder.Entity<Penalizacion>(b =>
        {
            b.ToTable("Penalizaciones");
            b.HasKey(x => x.Id);

            // Importante: evitar truncamiento
            b.Property(x => x.Monto)
                .HasPrecision(18, 2);

            // Opcional pero recomendable: Motivo/Estado como int (por defecto ya lo hace)
            // b.Property(x => x.Estado).HasConversion<int>();
            // b.Property(x => x.Motivo).HasConversion<int>();
        });

        modelBuilder.Entity<Notificacion>(b =>
        {
            b.ToTable("Notificaciones");
            b.HasKey(x => x.Id);

            b.Property(x => x.Mensaje)
                .HasMaxLength(500)
                .IsRequired();
        });

        // Si luego se configurar más entidades aquí, se hace igual.
    }
}