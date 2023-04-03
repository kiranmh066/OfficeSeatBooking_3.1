using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Office_Seat_Book_Entity
{
    public class Help
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HelpId { get; set; }


        [ForeignKey("Employee")]
        public int EmpID { get; set; }
        public Employee Employee { get; set; }

        [AllowNull]
        public string TypeOfQuery { get; set; }

        [AllowNull]
        public string Description { get; set; }
    }
}
