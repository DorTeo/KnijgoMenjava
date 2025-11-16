using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnijgoMenjava.Migrations
{
    /// <inheritdoc />
    public partial class OdpravaRelacij : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_Uporabnik_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_Uporabnik_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_Uporabnik_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_Uporabnik_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Knjiga_Kategorija_KategorijaId",
                table: "Knjiga");

            migrationBuilder.DropForeignKey(
                name: "FK_Knjiga_Uporabnik_LastnikId",
                table: "Knjiga");

            migrationBuilder.DropForeignKey(
                name: "FK_Ocena_Knjiga_KnjigaId",
                table: "Ocena");

            migrationBuilder.DropForeignKey(
                name: "FK_Ocena_Uporabnik_UporabnikId",
                table: "Ocena");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_Knjiga_KnjigaId",
                table: "Rezervacija");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_Uporabnik_UporabnikId",
                table: "Rezervacija");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_Uporabnik_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "Uporabnik",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_Uporabnik_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "Uporabnik",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_Uporabnik_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "Uporabnik",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_Uporabnik_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "Uporabnik",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Knjiga_Kategorija_KategorijaId",
                table: "Knjiga",
                column: "KategorijaId",
                principalTable: "Kategorija",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Knjiga_Uporabnik_LastnikId",
                table: "Knjiga",
                column: "LastnikId",
                principalTable: "Uporabnik",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ocena_Knjiga_KnjigaId",
                table: "Ocena",
                column: "KnjigaId",
                principalTable: "Knjiga",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ocena_Uporabnik_UporabnikId",
                table: "Ocena",
                column: "UporabnikId",
                principalTable: "Uporabnik",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_Knjiga_KnjigaId",
                table: "Rezervacija",
                column: "KnjigaId",
                principalTable: "Knjiga",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_Uporabnik_UporabnikId",
                table: "Rezervacija",
                column: "UporabnikId",
                principalTable: "Uporabnik",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_Uporabnik_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_Uporabnik_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_Uporabnik_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_Uporabnik_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Knjiga_Kategorija_KategorijaId",
                table: "Knjiga");

            migrationBuilder.DropForeignKey(
                name: "FK_Knjiga_Uporabnik_LastnikId",
                table: "Knjiga");

            migrationBuilder.DropForeignKey(
                name: "FK_Ocena_Knjiga_KnjigaId",
                table: "Ocena");

            migrationBuilder.DropForeignKey(
                name: "FK_Ocena_Uporabnik_UporabnikId",
                table: "Ocena");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_Knjiga_KnjigaId",
                table: "Rezervacija");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_Uporabnik_UporabnikId",
                table: "Rezervacija");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_Uporabnik_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "Uporabnik",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_Uporabnik_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "Uporabnik",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_Uporabnik_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "Uporabnik",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_Uporabnik_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "Uporabnik",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Knjiga_Kategorija_KategorijaId",
                table: "Knjiga",
                column: "KategorijaId",
                principalTable: "Kategorija",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Knjiga_Uporabnik_LastnikId",
                table: "Knjiga",
                column: "LastnikId",
                principalTable: "Uporabnik",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ocena_Knjiga_KnjigaId",
                table: "Ocena",
                column: "KnjigaId",
                principalTable: "Knjiga",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ocena_Uporabnik_UporabnikId",
                table: "Ocena",
                column: "UporabnikId",
                principalTable: "Uporabnik",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_Knjiga_KnjigaId",
                table: "Rezervacija",
                column: "KnjigaId",
                principalTable: "Knjiga",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_Uporabnik_UporabnikId",
                table: "Rezervacija",
                column: "UporabnikId",
                principalTable: "Uporabnik",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
