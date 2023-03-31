﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Office_Seat_Book_DLL;

namespace Office_Seat_Book_DLL.Migrations
{
    [DbContext(typeof(Office_DB_Context))]
    partial class Office_DB_ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Office_Seat_Book_Entity.Booking", b =>
                {
                    b.Property<int>("BookingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Emp_Status")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<int>("Food_Type")
                        .HasColumnType("int");

                    b.Property<DateTime>("From_Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Seat_No")
                        .HasColumnType("int");

                    b.Property<string>("Shift_Time")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("To_Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type_Of_Request")
                        .HasColumnType("int");

                    b.Property<bool>("Vehicle")
                        .HasColumnType("bit");

                    b.Property<int>("booking_Status")
                        .HasColumnType("int");

                    b.HasKey("BookingID");

                    b.HasIndex("EmployeeID");

                    b.HasIndex("Seat_No");

                    b.ToTable("booking");
                });

            modelBuilder.Entity("Office_Seat_Book_Entity.Employee", b =>
                {
                    b.Property<int>("EmpID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Designation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PhoneNo")
                        .HasColumnType("float");

                    b.Property<string>("Place")
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Secret_Key")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmpID");

                    b.ToTable("employee");
                });

            modelBuilder.Entity("Office_Seat_Book_Entity.Floor", b =>
                {
                    b.Property<int>("FloorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FloorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FloorID");

                    b.ToTable("floor");
                });

            modelBuilder.Entity("Office_Seat_Book_Entity.Parking", b =>
                {
                    b.Property<int>("ParkingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("BookingID")
                        .HasColumnType("int");

                    b.Property<string>("ParkingType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ParkingID");

                    b.HasIndex("BookingID");

                    b.ToTable("parking");
                });

            modelBuilder.Entity("Office_Seat_Book_Entity.Seat", b =>
                {
                    b.Property<int>("Seat_No")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("FloorID")
                        .HasColumnType("int");

                    b.Property<bool>("seat_flag")
                        .HasColumnType("bit");

                    b.HasKey("Seat_No");

                    b.HasIndex("FloorID");

                    b.ToTable("seat");
                });

            modelBuilder.Entity("Office_Seat_Book_Entity.SecretKey", b =>
                {
                    b.Property<int>("SecretId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("EmpID")
                        .HasColumnType("int");

                    b.Property<string>("SpecialKey")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SecretId");

                    b.HasIndex("EmpID");

                    b.ToTable("secretKey");
                });

            modelBuilder.Entity("Office_Seat_Book_Entity.Booking", b =>
                {
                    b.HasOne("Office_Seat_Book_Entity.Employee", "employee")
                        .WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Office_Seat_Book_Entity.Seat", "seat")
                        .WithMany()
                        .HasForeignKey("Seat_No")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("employee");

                    b.Navigation("seat");
                });

            modelBuilder.Entity("Office_Seat_Book_Entity.Parking", b =>
                {
                    b.HasOne("Office_Seat_Book_Entity.Booking", "booking")
                        .WithMany()
                        .HasForeignKey("BookingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("booking");
                });

            modelBuilder.Entity("Office_Seat_Book_Entity.Seat", b =>
                {
                    b.HasOne("Office_Seat_Book_Entity.Floor", "Floor")
                        .WithMany()
                        .HasForeignKey("FloorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Floor");
                });

            modelBuilder.Entity("Office_Seat_Book_Entity.SecretKey", b =>
                {
                    b.HasOne("Office_Seat_Book_Entity.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmpID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });
#pragma warning restore 612, 618
        }
    }
}
