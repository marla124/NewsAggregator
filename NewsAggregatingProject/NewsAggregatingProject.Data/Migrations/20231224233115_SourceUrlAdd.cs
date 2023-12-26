using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsAggregatingProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class SourceUrlAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SourceUrl",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceUrl",
                table: "News");
        }
    }
}
