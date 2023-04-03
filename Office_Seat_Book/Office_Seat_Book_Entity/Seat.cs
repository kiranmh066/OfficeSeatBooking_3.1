using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Office_Seat_Book_Entity
{
    public class Seat
    {
        [Key]
       public int Seat_No { get; set; }
        public bool Seat_flag { get; set; }

        [ForeignKey("Floor")]
        public int FloorID { get; set; }
        public Floor Floor { get; set; }
    }
}
