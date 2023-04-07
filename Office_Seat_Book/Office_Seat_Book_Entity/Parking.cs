using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Diagnostics.CodeAnalysis;

namespace Office_Seat_Book_Entity
{
    public class Parking
    {
        [Key]
        public int ParkingID { get; set; }


        [AllowNull]
        public string ParkingType { get; set; }

        [ForeignKey("booking")]
        public int BookingID { get; set; }
        public Booking booking { get; set; }
        public int Parking_Number { get; set; }
    }
}
