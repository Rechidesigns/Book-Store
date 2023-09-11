using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test_Project2.Migrations
{
    /// <inheritdoc />
    public partial class InitializeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books_Table",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Authors_Name = table.Column<string>(type: "text", nullable: true),
                    Books_Title = table.Column<string>(type: "text", nullable: true),
                    Cover_Image_Url = table.Column<string>(type: "text", nullable: true),
                    Books_File = table.Column<string>(type: "text", nullable: true),
                    Published_Date = table.Column<string>(type: "text", nullable: true),
                    Created_On = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books_Table", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books_Table");
        }
    }
}
