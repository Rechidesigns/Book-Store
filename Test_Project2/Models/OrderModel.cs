namespace Test_Project2.Models
{
    public class Order_Model
    {
        public Guid Id { get; set; }
        public Guid Customer_Id { get; set; }
        public string? Order_Status { get; set; }
        public Guid Book_Id { get; set; }
        public Guid Cart_Id { get; set; }
        public string? Total_Amount { get; set; }
        public int? Total_Quantity { get; set; }
        public DateTime Created_On { get; set; }
    }

    public class Order_ModelDto
    {
        public Guid Id { get; set; }
        public Guid Customer_Id { get; set; }
        public string? Order_Status { get; set; }
        public Guid Book_Id { get; set; }
        public Guid Cart_Id { get; set; }
        public string? Total_Amount { get; set; }
        public int? Total_Quantity { get; set; }
        public DateTime Created_On { get; set; }
    }
}
