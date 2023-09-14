namespace Test_Project2.Models
{
    public class Customer_Details
    {
        public Guid Id { get; set; }
        public string? First_Name { get; set; }
        public string? Last_Name { get; set; }
        public string? Email { get; set; }
        public string? Phone_Number { get; set; }
        public string? Address_Line_1 { get; set; }
        public string? Address_Line_2 { get;  set; }
        public string? Profile_Picture { get; set; }
        public DateTime Created_On { get; set; }

    }

    public class Customer_DetailsDto
    {
        public Guid Id { get; set; }
        public string? First_Name { get; set; }
        public string? Last_Name { get; set; }
        public string? Email { get; set; }
        public string? Phone_Number { get; set; }
        public string? Address_Line_1 { get; set; }
        public string? Address_Line_2 { get; set; }
        public string? Profile_Picture { get; set; }
        public DateTime Created_On { get; set; }

    }
}



