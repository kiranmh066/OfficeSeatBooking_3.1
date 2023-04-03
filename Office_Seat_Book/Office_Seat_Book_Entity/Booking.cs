using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Office_Seat_Book_Entity
{
    public class Booking
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingID { get; set; }

        [ForeignKey("employee")]
        public int EmployeeID { get; set; }
        public Employee employee { get; set; }


        [Required]
        public int Food_Type { get; set; }

        [Required]
        public int Type_Of_Request { get; set; }

        [Required]
        public DateTime From_Date { get; set; }

        [Required]
        public DateTime To_Date { get; set; }

        [Required]
        public string Shift_Time { get; set; }

        [ForeignKey("seat")]
        public int Seat_No { get; set; }
        public Seat seat { get; set; }

        [Required]
        public int booking_Status { get; set; }


        [Required]
        public int Emp_Status { get; set; }

        [Required]
        public bool Vehicle { get; set; }
    }
}
