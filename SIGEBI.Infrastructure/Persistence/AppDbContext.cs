using Microsoft.EntityFrameworkCore;
using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Entities.Dbo;

namespace SIGEBI.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Recurso> Recursos => Set<Recurso>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Prestamo> Prestamos => Set<Prestamo>();
    public DbSet<Ejemplar> Ejemplares => Set<Ejemplar>();
    public DbSet<Reserva> Reservas => Set<Reserva>();   

    public DbSet<Auditoria> Auditorias => Set<Auditoria>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =========================
        // AUDITORIA
        // =========================
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

        // =========================
        // RESERVA
        // =========================
        modelBuilder.Entity<Reserva>(b =>
        {
            b.ToTable("Reservas");

            b.HasKey(x => x.Id);

            b.Property(x => x.UsuarioId).IsRequired();
            b.Property(x => x.EjemplarId).IsRequired();
            b.Property(x => x.FechaCreacionUtc).IsRequired();

            //  unico: un usuario no puede reservar el mismo ejemplar activo
            b.HasIndex(x => new { x.UsuarioId, x.EjemplarId })
                .HasFilter("[FechaCancelacionUtc] IS NULL")
                .IsUnique();
        });

        // Si luego se configuran más entidades aquí, se hace igual.
    }
}