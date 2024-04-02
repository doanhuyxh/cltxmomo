using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cltxmomo.Migrations
{
    public partial class addtoop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Top",
                table: "TopDay",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Top",
                table: "TopDay");
        }
    }
}
