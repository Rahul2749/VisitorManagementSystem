using System.ComponentModel.DataAnnotations;

namespace VisitorManagementSystem.Models
{
    public class VTypeModel
    {
        [Key]
        public int TypeId { get; set; }
        public string? VisitorType { get; set; }
    }
}
