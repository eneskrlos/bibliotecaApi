using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace bibliotecaApi.Migrations
{
    /// <inheritdoc />
    public partial class ArregloEnReacionesCreacionSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prestamo_Lector_LectorId1",
                table: "Prestamo");

            migrationBuilder.DropForeignKey(
                name: "FK_Prestamo_Libro_LibroId",
                table: "Prestamo");

            migrationBuilder.DropIndex(
                name: "IX_Prestamo_LectorId1",
                table: "Prestamo");

            migrationBuilder.DropIndex(
                name: "IX_Prestamo_LibroId",
                table: "Prestamo");

            migrationBuilder.DropColumn(
                name: "LectorId1",
                table: "Prestamo");

            migrationBuilder.DropColumn(
                name: "LibroId",
                table: "Prestamo");

            migrationBuilder.InsertData(
                table: "Lector",
                columns: new[] { "id", "Nombre" },
                values: new object[,]
                {
                    { new Guid("753b8f78-0eeb-4b89-befe-de9d1aa97903"), "Carlos" },
                    { new Guid("9623908d-6eff-4f1e-9300-5a17b1369b1d"), "Ernesto" }
                });

            migrationBuilder.InsertData(
                table: "Libro",
                columns: new[] { "id", "ISBN", "Nombre", "prestado" },
                values: new object[] { new Guid("afaa7d0c-62ac-4b94-b66a-87d71584a23f"), "1234567890", "Pinocho", false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lector",
                keyColumn: "id",
                keyValue: new Guid("753b8f78-0eeb-4b89-befe-de9d1aa97903"));

            migrationBuilder.DeleteData(
                table: "Lector",
                keyColumn: "id",
                keyValue: new Guid("9623908d-6eff-4f1e-9300-5a17b1369b1d"));

            migrationBuilder.DeleteData(
                table: "Libro",
                keyColumn: "id",
                keyValue: new Guid("afaa7d0c-62ac-4b94-b66a-87d71584a23f"));

            migrationBuilder.AddColumn<Guid>(
                name: "LectorId1",
                table: "Prestamo",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LibroId",
                table: "Prestamo",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prestamo_LectorId1",
                table: "Prestamo",
                column: "LectorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamo_LibroId",
                table: "Prestamo",
                column: "LibroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamo_Lector_LectorId1",
                table: "Prestamo",
                column: "LectorId1",
                principalTable: "Lector",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamo_Libro_LibroId",
                table: "Prestamo",
                column: "LibroId",
                principalTable: "Libro",
                principalColumn: "id");
        }
    }
}
