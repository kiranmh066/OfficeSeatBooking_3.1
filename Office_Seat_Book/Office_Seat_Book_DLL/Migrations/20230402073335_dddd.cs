using Microsoft.EntityFrameworkCore.Migrations;

namespace Office_Seat_Book_DLL.Migrations
{
    public partial class dddd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Designation",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "Place",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "Emp_Status",
                table: "booking");

            migrationBuilder.AddColumn<bool>(
                name: "EmployeeStatus",
                table: "employee",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeStatus",
                table: "employee");

            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "employee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Place",
                table: "employee",
                type: "varchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Emp_Status",
                table: "booking",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
