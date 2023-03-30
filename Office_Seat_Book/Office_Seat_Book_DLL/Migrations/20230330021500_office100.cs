using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Office_Seat_Book_DLL.Migrations
{
    public partial class office100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    EmpID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(30)", nullable: true),
                    PhoneNo = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Secret_Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Place = table.Column<string>(type: "varchar(30)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.EmpID);
                });

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

            migrationBuilder.CreateTable(
                name: "booking",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    Food_Type = table.Column<int>(type: "int", nullable: false),
                    Type_Of_Request = table.Column<int>(type: "int", nullable: false),
                    From_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    To_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Shift_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Seat_No = table.Column<int>(type: "int", nullable: false),
                    booking_Status = table.Column<int>(type: "int", nullable: false),
                    Emp_Status = table.Column<bool>(type: "bit", nullable: false),
                    Vehicle = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK_booking_employee_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "employee",
                        principalColumn: "EmpID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_booking_seat_Seat_No",
                        column: x => x.Seat_No,
                        principalTable: "seat",
                        principalColumn: "Seat_No",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "parking",
                columns: table => new
                {
                    ParkingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParkingType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookingID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parking", x => x.ParkingID);
                    table.ForeignKey(
                        name: "FK_parking_booking_BookingID",
                        column: x => x.BookingID,
                        principalTable: "booking",
                        principalColumn: "BookingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_booking_EmployeeID",
                table: "booking",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_booking_Seat_No",
                table: "booking",
                column: "Seat_No");

            migrationBuilder.CreateIndex(
                name: "IX_parking_BookingID",
                table: "parking",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_seat_FloorID",
                table: "seat",
                column: "FloorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "parking");

            migrationBuilder.DropTable(
                name: "booking");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "seat");

            migrationBuilder.DropTable(
                name: "floor");
        }
    }
}
