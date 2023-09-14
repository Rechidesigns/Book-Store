using System.ComponentModel.DataAnnotations;

namespace Test_Project2.Models.UserModel
{
    public class Forgot_Password_Request
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }
    }
}
