namespace Test_Project2.Models
{
    public class Books_Model
    {
        public Guid Id { get; set; }
        public string? Authors_Name { get; set; }
        public string? Books_Title { get; set; }
        public string? Cover_Image_Url { get; set; }
        public string? Books_File { get; set; }
        public int? Books_Quantity { get; set; }
        public string? Book_Price { get; set; }
        public string? Published_Date { get; set; }
        public DateTime Created_On { get; set; }
    }


    public class Books_ModelDto
    {
        public Guid Id { get; set; }
        public string? Authors_Name { get; set; }
        public string? Books_Title { get; set; }
        public string? Cover_Image_Url { get; set; }
        public string? Books_File { get; set; }
        public int? Books_Quantity { get; set; }
        public string? Book_Price { get; set; }
        public string? Published_Date { get; set; }
        public DateTime Created_On { get; set; }
    }
}


