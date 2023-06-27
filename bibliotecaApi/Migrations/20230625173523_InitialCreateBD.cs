using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bibliotecaApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateBD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lector",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Nombre = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Lector__3213E83F1DEED1A4", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Libro",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Nombre = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ISBM = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    prestado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Libro__3213E83F6980B911", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Prestamo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    FechaPrestamo = table.Column<DateTime>(type: "datetime", nullable: false),
                    IdLibro = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LectorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LectorId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LibroId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Prestamo__3213E83F1980B911", x => x.id);
                    table.ForeignKey(
                        name: "FK_Prest_Lector",
                        column: x => x.LectorId,
                        principalTable: "Lector",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prest_Libro",
                        column: x => x.IdLibro,
                        principalTable: "Libro",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prestamo_Lector_LectorId1",
                        column: x => x.LectorId1,
                        principalTable: "Lector",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Prestamo_Libro_LibroId",
                        column: x => x.LibroId,
                        principalTable: "Libro",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prestamo_IdLibro",
                table: "Prestamo",
                column: "IdLibro");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamo_LectorId",
                table: "Prestamo",
                column: "LectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamo_LectorId1",
                table: "Prestamo",
                column: "LectorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamo_LibroId",
                table: "Prestamo",
                column: "LibroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prestamo");

            migrationBuilder.DropTable(
                name: "Lector");

            migrationBuilder.DropTable(
                name: "Libro");
        }
    }
}
