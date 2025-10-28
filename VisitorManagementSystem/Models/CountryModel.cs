using System.ComponentModel.DataAnnotations;

namespace VisitorManagementSystem.Models
{
    public class CountryModel
    {
        [Key]
        public int CountryId { get; set; }
        public string? CountryName { get; set; }
    }
}
