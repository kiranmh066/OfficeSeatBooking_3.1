using Microsoft.EntityFrameworkCore.Migrations;

namespace Office_Seat_Book_DLL.Migrations
{
    public partial class dance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "seat_flag",
                table: "seat",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "seat_flag",
                table: "seat");
        }
    }
}
