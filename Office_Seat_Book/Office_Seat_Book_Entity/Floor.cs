using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Office_Seat_Book_Entity
{
    public class Floor
    {
        [Key]
        public int FloorID { get; set; }

        [AllowNull]
        public string FloorName { get; set; }
    }
}
