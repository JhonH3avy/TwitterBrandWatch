using Microsoft.EntityFrameworkCore.Migrations;

namespace TweeterSearchApp.Migrations
{
    public partial class DefaultTweetIdAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tweets",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "TweetId",
                table: "Tweets");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Tweets",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tweets",
                table: "Tweets",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tweets",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Tweets");

            migrationBuilder.AddColumn<decimal>(
                name: "TweetId",
                table: "Tweets",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tweets",
                table: "Tweets",
                column: "TweetId");
        }
    }
}
