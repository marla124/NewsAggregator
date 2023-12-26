using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsAggregatingProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeleteTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Categories_CategoryId",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_News_RatingScales_RatingScaleId",
                table: "News");

            migrationBuilder.DropTable(
                name: "RatingScales");

            migrationBuilder.DropIndex(
                name: "IX_News_RatingScaleId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "RatingScaleId",
                table: "News");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "News",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Rate",
                table: "News",
                type: "real",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_News_Categories_CategoryId",
                table: "News",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Categories_CategoryId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "News");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "News",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "RatingScaleId",
                table: "News",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RatingScales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingScales", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_News_RatingScaleId",
                table: "News",
                column: "RatingScaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Categories_CategoryId",
                table: "News",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_News_RatingScales_RatingScaleId",
                table: "News",
                column: "RatingScaleId",
                principalTable: "RatingScales",
                principalColumn: "Id");
        }
    }
}
