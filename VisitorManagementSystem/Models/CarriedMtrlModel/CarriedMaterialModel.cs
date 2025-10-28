using System.ComponentModel.DataAnnotations;

namespace VisitorManagementSystem.Models.CarriedMtrlModel
{
    public class CarriedMaterialModel
    {
        [Key]
        public int Id { get; set; }
        public string? cMtrl { get; set; }


        public int cMtrlQuantity { get; set; }
        public string? VisitorId { get; set; }

        public DateOnly VisitDate { get; set; }
        public DateTime? DateTime { get; set; }
    }
}
