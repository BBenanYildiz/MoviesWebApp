using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies.Repository.Migrations
{
    public partial class tables_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_MovieReviews_MovieReviewId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_MovieReviews_MovieReviewId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_MovieReviewId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Movies_MovieReviewId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "MovieReviewId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MovieReviewId",
                table: "Movies");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "MovieReviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "MovieReviews",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "MovieReviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "MovieReviews");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "MovieReviews");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "MovieReviews");

            migrationBuilder.AddColumn<int>(
                name: "MovieReviewId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieReviewId",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_MovieReviewId",
                table: "Users",
                column: "MovieReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_MovieReviewId",
                table: "Movies",
                column: "MovieReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_MovieReviews_MovieReviewId",
                table: "Movies",
                column: "MovieReviewId",
                principalTable: "MovieReviews",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_MovieReviews_MovieReviewId",
                table: "Users",
                column: "MovieReviewId",
                principalTable: "MovieReviews",
                principalColumn: "Id");
        }
    }
}
