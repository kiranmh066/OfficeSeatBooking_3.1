using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [AllowNull]
        public byte[] Qr { get; set; }
    }
}
