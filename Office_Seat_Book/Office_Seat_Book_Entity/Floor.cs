using System;
using System.ComponentModel.DataAnnotations;

namespace Office_Seat_Book_Entity
{
    public class Floor
    {
        [Key]
        /*[DatabaseGenerated(DatabaseGeneratedOption.Identity)]*/
        public int FloorID { get; set; }

        [Required]
        public string FloorName { get; set; }
    }
}
