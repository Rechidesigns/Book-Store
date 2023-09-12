namespace Test_Project2.Models
{
    public class AddOrder
    {
        public Guid Id { get; set; }
        public Guid Customer_Id { get; set; }
        public Guid Book_Id { get; set; }
        public Guid Cart_Id { get; set; }
        public string? Total_Amount { get; set; }
    }

    public class UpdateOrder
    {
        public string? Order_Status { get; set; }
    }
}
