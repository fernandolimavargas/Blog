using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogFecomercio.Migrations
{
    public partial class AjustesFKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagsID",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Tags",
                newName: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "Tags",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "TagsID",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
