using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIGEBI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixPenalizacionPrestamoIdToGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrestamoId",
                table: "Penalizaciones");

            migrationBuilder.AddColumn<Guid>(
                name: "PrestamoId",
                table: "Penalizaciones",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrestamoId",
                table: "Penalizaciones");

            migrationBuilder.AddColumn<int>(
                name: "PrestamoId",
                table: "Penalizaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
