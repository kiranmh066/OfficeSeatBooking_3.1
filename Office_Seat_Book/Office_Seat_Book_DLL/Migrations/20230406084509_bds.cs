using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Office_Seat_Book_DLL.Migrations
{
    public partial class bds : Migration
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
                    PhoneNo = table.Column<double>(type: "float", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Security_Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeStatus = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
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
                    FloorName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_floor", x => x.FloorID);
                });

            migrationBuilder.CreateTable(
                name: "help",
                columns: table => new
                {
                    HelpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpID = table.Column<int>(type: "int", nullable: false),
                    TypeOfQuery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_help", x => x.HelpId);
                    table.ForeignKey(
                        name: "FK_help_employee_EmpID",
                        column: x => x.EmpID,
                        principalTable: "employee",
                        principalColumn: "EmpID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "secretKey",
                columns: table => new
                {
                    SecretId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpID = table.Column<int>(type: "int", nullable: false),
                    SpecialKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qr = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_secretKey", x => x.SecretId);
                    table.ForeignKey(
                        name: "FK_secretKey_employee_EmpID",
                        column: x => x.EmpID,
                        principalTable: "employee",
                        principalColumn: "EmpID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "seat",
                columns: table => new
                {
                    Seat_No = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Seat_flag = table.Column<bool>(type: "bit", nullable: false),
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
                    Shift_Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seat_No = table.Column<int>(type: "int", nullable: false),
                    Booking_Status = table.Column<int>(type: "int", nullable: false),
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
                    Parking_Number = table.Column<int>(type: "int", nullable: false),
                    ParkingType = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "IX_help_EmpID",
                table: "help",
                column: "EmpID");

            migrationBuilder.CreateIndex(
                name: "IX_parking_BookingID",
                table: "parking",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_seat_FloorID",
                table: "seat",
                column: "FloorID");

            migrationBuilder.CreateIndex(
                name: "IX_secretKey_EmpID",
                table: "secretKey",
                column: "EmpID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "help");

            migrationBuilder.DropTable(
                name: "parking");

            migrationBuilder.DropTable(
                name: "secretKey");

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
