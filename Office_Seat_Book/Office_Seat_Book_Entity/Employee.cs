using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Office_Seat_Book_Entity
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpID { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Name { get; set; }

        public double PhoneNo { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public char Gender { get; set; }

        public string Secret_Key { get; set; }

        public string Role { get; set; }

        public bool EmployeeStatus { get;set; }

    }
}
