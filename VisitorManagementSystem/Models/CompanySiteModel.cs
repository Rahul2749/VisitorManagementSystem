using System.ComponentModel.DataAnnotations;

namespace VisitorManagementSystem.Models
{
    public class CompanySiteModel
    {
        [Key]
        public int SiteId { get; set; }
        public string? SiteName { get; set; }

        public int CountryId { get; set; }
    }
}
