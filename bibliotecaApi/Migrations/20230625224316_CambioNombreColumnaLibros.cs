using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bibliotecaApi.Migrations
{
    /// <inheritdoc />
    public partial class CambioNombreColumnaLibros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ISBM",
                table: "Libro",
                newName: "ISBN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ISBN",
                table: "Libro",
                newName: "ISBM");
        }
    }
}
