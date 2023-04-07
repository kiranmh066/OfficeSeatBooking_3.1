using Microsoft.EntityFrameworkCore;
using Office_Seat_Book_Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Office_Seat_Book_DLL
{
    public class Office_DB_Context : DbContext
    {
        public Office_DB_Context()
        {
        }
        public Office_DB_Context(DbContextOptions<Office_DB_Context> options) : base(options)
        {
        }



        public Microsoft.EntityFrameworkCore.DbSet<Parking> parking { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Employee> employee { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Booking> booking { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Floor> floor { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Seat> seat { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<SecretKey> secretKey { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Help> help { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {

            dbContextOptionsBuilder.UseSqlServer("Data Source =DESKTOP-FICLE82\\SQLEXPRESS; Initial Catalog =Officeseen3; Integrated Security = True;");





        }

    }
}
