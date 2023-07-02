using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payne.Migrations
{
    public partial class DeleteTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Choses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Choses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
