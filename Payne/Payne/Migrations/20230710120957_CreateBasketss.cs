using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payne.Migrations
{
    public partial class CreateBasketss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BasketItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BasketItems",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
