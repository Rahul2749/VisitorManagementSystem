using System.ComponentModel.DataAnnotations;


namespace VisitorManagementSystem.Models
{
    public class DeptData
    {
        [Key]
        public int DeptId { get; set; }
        public string? DeptName { get; set; }

        public string? DeptHOD { get; set; }


    }
}
