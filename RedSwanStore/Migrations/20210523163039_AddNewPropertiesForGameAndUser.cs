using Microsoft.EntityFrameworkCore.Migrations;

namespace RedSwanStore.Migrations
{
    public partial class AddNewPropertiesForGameAndUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserUrl",
                table: "Users",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GameUrl",
                table: "Games",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserUrl",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GameUrl",
                table: "Games");
        }
    }
}
