using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnijgoMenjava.Migrations
{
    /// <inheritdoc />
    public partial class PopravjenCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ocena_Knjiga_KnjigaId",
                table: "Ocena");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_Knjiga_KnjigaId",
                table: "Rezervacija");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DatumVrnitve",
                table: "Rezervacija",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ocena_Knjiga_KnjigaId",
                table: "Ocena",
                column: "KnjigaId",
                principalTable: "Knjiga",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_Knjiga_KnjigaId",
                table: "Rezervacija",
                column: "KnjigaId",
                principalTable: "Knjiga",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ocena_Knjiga_KnjigaId",
                table: "Ocena");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_Knjiga_KnjigaId",
                table: "Rezervacija");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DatumVrnitve",
                table: "Rezervacija",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_Ocena_Knjiga_KnjigaId",
                table: "Ocena",
                column: "KnjigaId",
                principalTable: "Knjiga",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_Knjiga_KnjigaId",
                table: "Rezervacija",
                column: "KnjigaId",
                principalTable: "Knjiga",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
