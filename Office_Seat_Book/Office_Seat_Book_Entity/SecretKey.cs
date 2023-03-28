using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Diagnostics.CodeAnalysis;

namespace Office_Seat_Book_Entity
{
    public class SecretKey
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SecretId { get; set; }


        [ForeignKey("Employee")]
        public int EmpID { get; set; }
        public Employee Employee { get; set; }

        [AllowNull]
        public string SpecialKey { get; set; }
    }
}
