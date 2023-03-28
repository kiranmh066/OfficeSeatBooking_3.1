using Microsoft.EntityFrameworkCore.Migrations;

namespace Office_Seat_Book_DLL.Migrations
{
    public partial class db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "floor",
                columns: table => new
                {
                    FloorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FloorName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_floor", x => x.FloorID);
                });

            migrationBuilder.CreateTable(
                name: "seat",
                columns: table => new
                {
                    Seat_No = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FloorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seat", x => x.Seat_No);
                    table.ForeignKey(
                        name: "FK_seat_floor_FloorID",
                        column: x => x.FloorID,
                        principalTable: "floor",
                        principalColumn: "FloorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_seat_FloorID",
                table: "seat",
                column: "FloorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "seat");

            migrationBuilder.DropTable(
                name: "floor");
        }
    }
}
