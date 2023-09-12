using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test_Project2.Migrations
{
    /// <inheritdoc />
    public partial class InitializedData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cart_Table",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Customer_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Book_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Total_Amount = table.Column<string>(type: "text", nullable: true),
                    Created_On = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart_Table", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer_Details_Table",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    First_Name = table.Column<string>(type: "text", nullable: true),
                    Last_Name = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Phone_Number = table.Column<string>(type: "text", nullable: true),
                    Address_Line_1 = table.Column<string>(type: "text", nullable: true),
                    Address_Line_2 = table.Column<string>(type: "text", nullable: true),
                    Created_On = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer_Details_Table", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order_Table",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Customer_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Order_Status = table.Column<string>(type: "text", nullable: true),
                    Book_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Cart_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Total_Amount = table.Column<string>(type: "text", nullable: true),
                    Created_On = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Table", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart_Table");

            migrationBuilder.DropTable(
                name: "Customer_Details_Table");

            migrationBuilder.DropTable(
                name: "Order_Table");
        }
    }
}
