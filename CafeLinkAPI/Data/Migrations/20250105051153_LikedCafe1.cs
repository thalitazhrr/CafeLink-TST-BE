using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeLinkAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class LikedCafe1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LikeCount",
                table: "Cafes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "Cafes");
        }
    }
}
