using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace saturnApp.Data.Migrations
{
    public partial class migracao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<long>(
                name: "RaizId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "No",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DiretorioId = table.Column<long>(type: "INTEGER", nullable: true),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    PaiId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_No", x => x.Id);
                    table.ForeignKey(
                        name: "FK_No_No_DiretorioId",
                        column: x => x.DiretorioId,
                        principalTable: "No",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_No_No_PaiId",
                        column: x => x.PaiId,
                        principalTable: "No",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Permissao",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ArquivoId = table.Column<long>(type: "INTEGER", nullable: false),
                    Escrita = table.Column<bool>(type: "INTEGER", nullable: false),
                    Leitura = table.Column<bool>(type: "INTEGER", nullable: false),
                    UsuarioId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissao_No_ArquivoId",
                        column: x => x.ArquivoId,
                        principalTable: "No",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permissao_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RaizId",
                table: "AspNetUsers",
                column: "RaizId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_No_DiretorioId",
                table: "No",
                column: "DiretorioId");

            migrationBuilder.CreateIndex(
                name: "IX_No_PaiId",
                table: "No",
                column: "PaiId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissao_ArquivoId",
                table: "Permissao",
                column: "ArquivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissao_UsuarioId",
                table: "Permissao",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_No_RaizId",
                table: "AspNetUsers",
                column: "RaizId",
                principalTable: "No",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_No_RaizId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Permissao");

            migrationBuilder.DropTable(
                name: "No");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RaizId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "RaizId",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}
