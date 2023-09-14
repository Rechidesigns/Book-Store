namespace Test_Project2.Models.UserModel
{
    public class LoginDto
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public string? Email { get; set; }
        public Guid UserId { get; set; }
        public string? UserType { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
