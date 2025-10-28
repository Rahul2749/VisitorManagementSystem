using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisitorManagementSystem.Models
{
    public class EmpData
    {
        [Key]
        public int EmpId { get; set; }
        public string? EmpName { get; set; }

        public string? KO { get; set; }
        public string? EmpEmail { get; set; }
        public string? EmpContactNo { get; set; }

        public int DeptId { get; set; }
        public int SiteId { get; set; }

       // [Column("userNAme")] Column Name in DB
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}
