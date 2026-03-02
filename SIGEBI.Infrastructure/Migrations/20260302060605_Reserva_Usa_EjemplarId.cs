using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIGEBI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Reserva_Usa_EjemplarId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecursoId",
                table: "Reservas",
                newName: "EjemplarId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservas_UsuarioId_RecursoId",
                table: "Reservas",
                newName: "IX_Reservas_UsuarioId_EjemplarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EjemplarId",
                table: "Reservas",
                newName: "RecursoId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservas_UsuarioId_EjemplarId",
                table: "Reservas",
                newName: "IX_Reservas_UsuarioId_RecursoId");
        }
    }
}
