using System.ComponentModel.DataAnnotations;

namespace VisitorManagementSystem.Models.LoginModels
{
    public class LoginModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide Email")]
        public string? Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide Password")]
        public string? Password { get; set; }
    }
}

