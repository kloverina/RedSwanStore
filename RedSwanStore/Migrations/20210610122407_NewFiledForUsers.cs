using Microsoft.EntityFrameworkCore.Migrations;

namespace RedSwanStore.Migrations
{
    public partial class NewFiledForUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentlyEditedGameId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentlyEditedGameId",
                table: "Users");
        }
    }
}
