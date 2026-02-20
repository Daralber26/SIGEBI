using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIGEBI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueActiveLoanIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        IF NOT EXISTS (
            SELECT 1
            FROM sys.indexes
            WHERE name = 'IX_Prestamos_EjemplarId_Activo'
              AND object_id = OBJECT_ID('dbo.Prestamos')
        )
        BEGIN
            CREATE UNIQUE INDEX IX_Prestamos_EjemplarId_Activo
            ON dbo.Prestamos(EjemplarId)
            WHERE FechaDevolucion IS NULL;
        END
    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        IF EXISTS (
            SELECT 1
            FROM sys.indexes
            WHERE name = 'IX_Prestamos_EjemplarId_Activo'
              AND object_id = OBJECT_ID('dbo.Prestamos')
        )
        BEGIN
            DROP INDEX IX_Prestamos_EjemplarId_Activo ON dbo.Prestamos;
        END
    ");
        }
    }
}