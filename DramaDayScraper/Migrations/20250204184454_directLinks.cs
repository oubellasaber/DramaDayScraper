using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DramaDayScraper.Migrations
{
    /// <inheritdoc />
    public partial class directLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DirectLink",
                table: "ShortLinks",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DirectLink",
                table: "ShortLinks");
        }
    }
}
