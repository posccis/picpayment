using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PicPayment.API.Migrations
{
    /// <inheritdoc />
    public partial class RealFirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Saldo = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transferencias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdContaOrigem = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdContaDestino = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Valor = table.Column<int>(type: "int", nullable: false),
                    DataTransferencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoTransferencia = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transferencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transferencias_Usuarios_IdContaDestino",
                        column: x => x.IdContaDestino,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transferencias_Usuarios_IdContaOrigem",
                        column: x => x.IdContaOrigem,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transferencias_Id",
                table: "Transferencias",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transferencias_IdContaDestino",
                table: "Transferencias",
                column: "IdContaDestino");

            migrationBuilder.CreateIndex(
                name: "IX_Transferencias_IdContaOrigem",
                table: "Transferencias",
                column: "IdContaOrigem");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Id_CPF_Email",
                table: "Usuarios",
                columns: new[] { "Id", "CPF", "Email" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transferencias");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
