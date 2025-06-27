using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTFG.Migrations
{
    /// <inheritdoc />
    public partial class Mig6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioHorario");

            migrationBuilder.DropTable(
                name: "Horarios");

            migrationBuilder.AddColumn<int>(
                name: "TareaDetalleId",
                table: "Eventos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TareasDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<double>(type: "float", nullable: false),
                    Unidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TareasDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TareasDetalles_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TareasActualizaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<double>(type: "float", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TareaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TareasActualizaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TareasActualizaciones_TareasDetalles_Id",
                        column: x => x.Id,
                        principalTable: "TareasDetalles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TareasActualizaciones_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TareasActualizaciones_UsuarioId",
                table: "TareasActualizaciones",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TareasDetalles_EventoId",
                table: "TareasDetalles",
                column: "EventoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TareasActualizaciones");

            migrationBuilder.DropTable(
                name: "TareasDetalles");

            migrationBuilder.DropColumn(
                name: "TareaDetalleId",
                table: "Eventos");

            migrationBuilder.CreateTable(
                name: "Horarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiaFin = table.Column<DateOnly>(type: "date", nullable: false),
                    DiaInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    Fin = table.Column<TimeOnly>(type: "time", nullable: false),
                    Inicio = table.Column<TimeOnly>(type: "time", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Horarios_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioHorario",
                columns: table => new
                {
                    HorariosId = table.Column<int>(type: "int", nullable: false),
                    UsuariosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioHorario", x => new { x.HorariosId, x.UsuariosId });
                    table.ForeignKey(
                        name: "FK_UsuarioHorario_Horarios_HorariosId",
                        column: x => x.HorariosId,
                        principalTable: "Horarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioHorario_Usuarios_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Horarios_EmpresaId",
                table: "Horarios",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioHorario_UsuariosId",
                table: "UsuarioHorario",
                column: "UsuariosId");
        }
    }
}
