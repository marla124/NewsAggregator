using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsAggregatingProject.Migrations
{
    /// <inheritdoc />
    public partial class AddElements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_RatingScales_RatingScaleId",
                table: "News");

            migrationBuilder.AlterColumn<float>(
                name: "Status",
                table: "RatingScales",
                type: "real",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "RatingScaleId",
                table: "News",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_News_RatingScales_RatingScaleId",
                table: "News",
                column: "RatingScaleId",
                principalTable: "RatingScales",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_RatingScales_RatingScaleId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "News");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "RatingScales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "RatingScaleId",
                table: "News",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_News_RatingScales_RatingScaleId",
                table: "News",
                column: "RatingScaleId",
                principalTable: "RatingScales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
