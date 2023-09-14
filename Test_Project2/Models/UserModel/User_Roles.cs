namespace Test_Project2.Models.UserModel
{
    public class User_Roles
    {
        public Guid Id { get; set; }

        public Guid GeneralUsersId { get; set; }
        public General_User? GeneralUsers { get; set; }
        public Guid RoleId { get; set; }
        public Roles? Roles { get; set; }
    }
}
