using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Test_Project2.Models.UserModel
{
    public class General_User
    {
        public Guid Id { get; set; }
        public string? SurName { get; set; }
        public string? FirstName { get; set; }
        public string? UserType { get; set; }
        public string? Email { get; set; }
        public string? PhoneNo { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? VerificationToken { get; set; }
        public bool? EmailConfirmed { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public string? PhoneVerificationToken { get; set; }
        public bool? PhoneNoConfirmed { get; set; }
        public DateTime? PhoneVerifiedAt { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public bool? TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockOutEnd { get; set; }
        public bool? LockOutEndEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string? ImagePath { get; set; }
        // navigation property
        [NotMapped]
        public List<string>? Roles { get; set; }
        public List<User_Roles>? User_Roles { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
