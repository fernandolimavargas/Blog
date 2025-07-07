using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogFecomercio.Migrations
{
    public partial class AjusteKeyCurtida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Curtidas",
                table: "Curtidas");

            migrationBuilder.DropIndex(
                name: "IX_Curtidas_UsuarioId",
                table: "Curtidas");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Usuarios",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Usuarios",
                newName: "Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Curtidas",
                table: "Curtidas",
                columns: new[] { "UsuarioId", "PostId" });

            migrationBuilder.CreateIndex(
                name: "IX_Curtidas_PostId",
                table: "Curtidas",
                column: "PostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Curtidas",
                table: "Curtidas");

            migrationBuilder.DropIndex(
                name: "IX_Curtidas_PostId",
                table: "Curtidas");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Usuarios",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Usuarios",
                newName: "email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Curtidas",
                table: "Curtidas",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Curtidas_UsuarioId",
                table: "Curtidas",
                column: "UsuarioId");
        }
    }
}
