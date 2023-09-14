using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test_Project2.Migrations
{
    /// <inheritdoc />
    public partial class InitializData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books_Table",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Authors_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Books_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cover_Image_Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Books_File = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Books_Quantity = table.Column<int>(type: "int", nullable: true),
                    Book_Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Published_Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_On = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books_Table", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cart_Table",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Customer_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Book_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Total_Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Book_Quantity = table.Column<int>(type: "int", nullable: true),
                    Created_On = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart_Table", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer_Details_Table",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    First_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Last_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Line_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Line_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Profile_Picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_On = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer_Details_Table", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "General_User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    VerificationToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: true),
                    VerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhoneVerificationToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNoConfirmed = table.Column<bool>(type: "bit", nullable: true),
                    PhoneVerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetTokenExpires = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: true),
                    LockOutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockOutEndEnabled = table.Column<bool>(type: "bit", nullable: true),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_General_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order_Table",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Customer_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Book_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cart_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Total_Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total_Quantity = table.Column<int>(type: "int", nullable: true),
                    Created_On = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Table", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User_Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GeneralUsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Roles_General_User_GeneralUsersId",
                        column: x => x.GeneralUsersId,
                        principalTable: "General_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Roles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_Roles_GeneralUsersId",
                table: "User_Roles",
                column: "GeneralUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Roles_RoleId",
                table: "User_Roles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books_Table");

            migrationBuilder.DropTable(
                name: "Cart_Table");

            migrationBuilder.DropTable(
                name: "Customer_Details_Table");

            migrationBuilder.DropTable(
                name: "Order_Table");

            migrationBuilder.DropTable(
                name: "User_Roles");

            migrationBuilder.DropTable(
                name: "General_User");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
