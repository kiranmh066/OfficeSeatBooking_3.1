using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Diagnostics.CodeAnalysis;

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


        [AllowNull]
        public int Food_Type { get; set; }
            
        [AllowNull]
        public int Type_Of_Request { get; set; }

        [AllowNull]
        public DateTime From_Date { get; set; }

        [AllowNull]
        public DateTime To_Date { get; set; }

        [AllowNull]
        public string Shift_Time { get; set; }

        [ForeignKey("seat")]
        public int Seat_No { get; set; }
        public Seat seat { get; set; }

        [AllowNull]
        public int booking_Status { get; set; }

        [Required]
        public bool Vehicle { get; set; }

    }
}
