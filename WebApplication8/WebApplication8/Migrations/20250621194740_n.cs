using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication8.Migrations
{
    /// <inheritdoc />
    public partial class n : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Participant",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[] { 2, "2332@gmail.com", "Jan", "Do" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Participant",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
