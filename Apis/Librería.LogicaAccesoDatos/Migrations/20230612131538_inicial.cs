using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Libreria.LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parametros",
                columns: table => new
                {
                    NombreParametro = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ValorDelParametro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parametros", x => x.NombreParametro);
                });

            migrationBuilder.CreateTable(
                name: "TipoCabania",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreDelTipo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostoHuesped = table.Column<int>(type: "int", nullable: false),
                    topeMinimo = table.Column<int>(type: "int", nullable: false),
                    topeMaximo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCabania", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pass = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cabania",
                columns: table => new
                {
                    NumeroHabitacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValorNombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TieneJacuzzi = table.Column<bool>(type: "bit", nullable: false),
                    TieneReservas = table.Column<bool>(type: "bit", nullable: false),
                    CantMaxPers_CantMaxPers = table.Column<int>(type: "int", nullable: false),
                    NombreFoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoId = table.Column<int>(type: "int", nullable: false),
                    topiMinimoCab = table.Column<int>(type: "int", nullable: false),
                    topiMaximoCab = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cabania", x => x.NumeroHabitacion);
                    table.ForeignKey(
                        name: "FK_Cabania_TipoCabania_TipoId",
                        column: x => x.TipoId,
                        principalTable: "TipoCabania",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mantenimiento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha_Valor = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Costo = table.Column<int>(type: "int", nullable: false),
                    NombreUs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CabaniaId = table.Column<int>(type: "int", nullable: false),
                    topeMinimoMtto = table.Column<int>(type: "int", nullable: false),
                    topeMaximoMtto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mantenimiento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mantenimiento_Cabania_CabaniaId",
                        column: x => x.CabaniaId,
                        principalTable: "Cabania",
                        principalColumn: "NumeroHabitacion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cabania_TipoId",
                table: "Cabania",
                column: "TipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cabania_ValorNombre",
                table: "Cabania",
                column: "ValorNombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimiento_CabaniaId",
                table: "Mantenimiento",
                column: "CabaniaId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoCabania_NombreDelTipo",
                table: "TipoCabania",
                column: "NombreDelTipo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "Parametros");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Cabania");

            migrationBuilder.DropTable(
                name: "TipoCabania");
        }
    }
}
