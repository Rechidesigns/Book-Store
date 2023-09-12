namespace Test_Project2.Models
{
    public class AddCart
    {
        public Guid Book_Id { get; set; }
        public string? Total_Amount { get; set; }
        public Guid Customer_Id { get; set; }

    }

    public class UpdateCart
    {
        public string? Total_Amount { get; set; }
    }
}
