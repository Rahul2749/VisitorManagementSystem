using System.ComponentModel.DataAnnotations;

namespace VisitorManagementSystem.Models
{
    public class VisitingDetailsModel
    {
        public int Id { get; set; }
        public DateOnly RegistrationDate { get; set; }

        [Required(ErrorMessage = "Please Select Visitor Type")]
        public int? TypeId { get; set; }


        //[Required(ErrorMessage = "Please Select Company Site")]
        public int CountryId { get; set; }
        public int CompanySite { get; set; }

        [Required(ErrorMessage = "Please Select Department")]
        public int? CompanyDepartment { get; set; }

        [Required(ErrorMessage = "Please Select To Visit")]
        public string? ApprovedBy { get; set; }

        [Required(ErrorMessage = "Please Enter Duration")]
        public string? Duration { get; set; }

        [Required(ErrorMessage = "Please Enter Purpose")]
        public string? Purpose { get; set; }

        [Required(ErrorMessage = "Please Enter Visiting Date")]

        public DateOnly VisitDate { get; set; }

        //[Required(ErrorMessage = "Please Enter Visiting Time")]
        public required string VisitTime { get; set; }

        [Required(ErrorMessage = "Please Select Visiting Area")]
        public bool VisitArea { get; set; }

        public DateOnly ToDate { get; set; }
        public string? SafetyStatus { get; set; }

        public string? SecurityApproval { get; set; }
        public string? Approval { get; set; }

        public required string VisitorId { get; set; }


    }

    
}
