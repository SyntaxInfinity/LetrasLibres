using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LetrasLibres.Migrations
{
    /// <inheritdoc />
    public partial class Tercera : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "rut",
                table: "Usuarios",
                newName: "Rut");

            migrationBuilder.RenameColumn(
                name: "celular",
                table: "Usuarios",
                newName: "Celular");

            migrationBuilder.RenameColumn(
                name: "apellido",
                table: "Usuarios",
                newName: "Apellido");

            migrationBuilder.RenameColumn(
                name: "estado",
                table: "Prestamo",
                newName: "Estado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rut",
                table: "Usuarios",
                newName: "rut");

            migrationBuilder.RenameColumn(
                name: "Celular",
                table: "Usuarios",
                newName: "celular");

            migrationBuilder.RenameColumn(
                name: "Apellido",
                table: "Usuarios",
                newName: "apellido");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "Prestamo",
                newName: "estado");
        }
    }
}
