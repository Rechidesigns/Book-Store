namespace Test_Project2.Models
{
    public class Cart_Model
    {
        public Guid Id { get; set; }
        public Guid Customer_Id { get; set; }
        public Guid Book_Id {  get; set; }
        public string? Total_Amount { get; set; }
        public DateTime Created_On {  get; set; }


    }

    public class Cart_ModelDto
    {
        public Guid Id { get; set; }
        public Guid Customer_Id { get; set; }
        public Guid Book_Id { get; set; }
        public string? Total_Amount { get; set; }



    }
}
