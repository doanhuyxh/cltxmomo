using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cltxmomo.Migrations
{
    public partial class themTopNgay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "HistoryWin");

            migrationBuilder.RenameColumn(
                name: "Game",
                table: "HistoryWin",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "AmountTailCode",
                table: "HistoryWin",
                newName: "Content");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "HistoryWin",
                newName: "Game");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "HistoryWin",
                newName: "AmountTailCode");

            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "HistoryWin",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
