using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Office_Seat_Book_Entity
{
    public class Parking
    {
        [Key]

        public int ParkingID { get; set; }


        public int Parking_Number { get; set; }
        [Required]
        public string ParkingType { get; set; }

        [ForeignKey("booking")]
        public int BookingID { get; set; }
        public Booking booking { get; set; }

    }
}
