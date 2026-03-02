using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIGEBI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueActiveReservaPerEjemplar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Reservas_EjemplarId_Activa' AND object_id = OBJECT_ID('dbo.Reservas'))
            BEGIN
                  CREATE UNIQUE INDEX IX_Reservas_EjemplarId_Activa
                  ON dbo.Reservas(EjemplarId)
                  WHERE FechaCancelacionUtc IS NULL;
             END
             ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Reservas_EjemplarId_Activa' AND object_id = OBJECT_ID('dbo.Reservas'))
            BEGIN
                DROP INDEX IX_Reservas_EjemplarId_Activa ON dbo.Reservas;
            END
            ");
        }
    }
}
