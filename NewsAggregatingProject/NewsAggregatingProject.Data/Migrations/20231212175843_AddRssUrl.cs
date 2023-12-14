using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsAggregatingProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRssUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Sources",
                newName: "RSSUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RSSUrl",
                table: "Sources",
                newName: "Description");
        }
    }
}
