using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LetrasLibres.Migrations
{
    /// <inheritdoc />
    public partial class SegundaRelacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_usuarios",
                table: "usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_prestamo",
                table: "prestamo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_libros",
                table: "libros");

            migrationBuilder.RenameTable(
                name: "usuarios",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "prestamo",
                newName: "Prestamo");

            migrationBuilder.RenameTable(
                name: "libros",
                newName: "Libros");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prestamo",
                table: "Prestamo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Libros",
                table: "Libros",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamo_LibroId",
                table: "Prestamo",
                column: "LibroId");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamo_UsuarioId",
                table: "Prestamo",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamo_Libros_LibroId",
                table: "Prestamo",
                column: "LibroId",
                principalTable: "Libros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamo_Usuarios_UsuarioId",
                table: "Prestamo",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prestamo_Libros_LibroId",
                table: "Prestamo");

            migrationBuilder.DropForeignKey(
                name: "FK_Prestamo_Usuarios_UsuarioId",
                table: "Prestamo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prestamo",
                table: "Prestamo");

            migrationBuilder.DropIndex(
                name: "IX_Prestamo_LibroId",
                table: "Prestamo");

            migrationBuilder.DropIndex(
                name: "IX_Prestamo_UsuarioId",
                table: "Prestamo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Libros",
                table: "Libros");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "usuarios");

            migrationBuilder.RenameTable(
                name: "Prestamo",
                newName: "prestamo");

            migrationBuilder.RenameTable(
                name: "Libros",
                newName: "libros");

            migrationBuilder.AddPrimaryKey(
                name: "PK_usuarios",
                table: "usuarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_prestamo",
                table: "prestamo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_libros",
                table: "libros",
                column: "Id");
        }
    }
}
