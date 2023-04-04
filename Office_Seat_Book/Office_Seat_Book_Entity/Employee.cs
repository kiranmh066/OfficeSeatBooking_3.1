using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

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

        public string Security_Question { get; set; }

        public string Role { get; set; }

        public bool EmployeeStatus { get;set; }
    }
}
