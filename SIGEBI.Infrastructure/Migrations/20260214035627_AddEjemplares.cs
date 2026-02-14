using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIGEBI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEjemplares : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EjemplarId",
                table: "Prestamos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Ejemplares",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecursoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ejemplares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ejemplares_Recursos_RecursoId",
                        column: x => x.RecursoId,
                        principalTable: "Recursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_EjemplarId",
                table: "Prestamos",
                column: "EjemplarId");

            migrationBuilder.CreateIndex(
                name: "IX_Ejemplares_RecursoId",
                table: "Ejemplares",
                column: "RecursoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamos_Ejemplares_EjemplarId",
                table: "Prestamos",
                column: "EjemplarId",
                principalTable: "Ejemplares",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prestamos_Ejemplares_EjemplarId",
                table: "Prestamos");

            migrationBuilder.DropTable(
                name: "Ejemplares");

            migrationBuilder.DropIndex(
                name: "IX_Prestamos_EjemplarId",
                table: "Prestamos");

            migrationBuilder.DropColumn(
                name: "EjemplarId",
                table: "Prestamos");
        }
    }
}
