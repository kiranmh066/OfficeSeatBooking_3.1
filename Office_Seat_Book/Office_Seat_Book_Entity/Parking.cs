using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Office_Seat_Book_Entity
{
    public class Parking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ParkingID { get; set; }


        [Required]
        public string ParkingType { get; set; }

        [ForeignKey("booking")]
        public int BookingID { get; set; }
        public Booking booking { get; set; }

    }
}
