using System.ComponentModel.DataAnnotations;

namespace VisitorManagementSystem.Models
{
    public class VisitorMasterModel
    {

        public int Id { get; set; }
        public required string VisitorId { get; set; } 

        [Required(ErrorMessage = "Please Enter First Name")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Last First Name")]
        public required string LastName { get; set; }

        //[Required(ErrorMessage = "Please Select DOB")]
        public DateOnly? DOB { get; set; }
        
        [Required(ErrorMessage = "Please Enter Company Name")]
        public string? Company { get; set; }

        [EmailValidation]
        public string? Email { get; set; }
        
        //[Required(ErrorMessage = "Please Enter Mobile Number"), Phone]
        //[StringLength(10, ErrorMessage = "Invalid Mobile Number"), MinLength(10, ErrorMessage = "Invalid Mobile Number")]
        public string? MobileNo { get; set; }

       

        [Required(ErrorMessage = "Please Select Gender")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Please Select Id Proof")]
        public string? IdProof { get; set; }

        [Required(ErrorMessage = "Please Enter Id Proof No.")]
        public string? IdProofNo { get; set; }

        [Required(ErrorMessage = "Please Provide Address.")]
        public string? Address { get; set; }
        public byte[]? ImageData { get; set; }


    }

    

    public class EmailValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var email = value as string;

            if (string.IsNullOrWhiteSpace(email))
            {
                return new ValidationResult("Please Enter Email Address or NA");
            }

            if (email.Trim().ToLower() == "na")
            {
                return ValidationResult.Success; // "na" is valid
            }

            if (!email.Contains("@"))
            {
                return new ValidationResult("Email must contain '@' symbol.");
            }

            return ValidationResult.Success; // Valid email
        }
    }
}
