using Test_Project2.Models.UserModel;

namespace Test_Project2.Models.UserModel
{
    public class Roles
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public List<User_Roles>? User_Roles { get; set; }
    }

}

