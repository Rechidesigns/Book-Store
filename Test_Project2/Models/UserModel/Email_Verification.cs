using System.ComponentModel.DataAnnotations;

namespace Test_Project2.Models.UserModel
{
    public class Email_Verification
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Token required")]
        public string? Token { get; set; }
        public string? Password { get; set; }
    }
}
