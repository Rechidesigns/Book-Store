using System.ComponentModel.DataAnnotations;

namespace Test_Project2.Models.UserModel
{
    public class Login_Request
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        // Attribute to validate the email address
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
