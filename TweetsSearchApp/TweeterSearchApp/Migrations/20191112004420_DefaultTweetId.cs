using Microsoft.EntityFrameworkCore.Migrations;

namespace TweeterSearchApp.Migrations
{
    public partial class DefaultTweetId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tweets",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "TweetId",
                table: "Tweets");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Tweets",
                nullable: false,
                defaultValue: 0)
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

            migrationBuilder.AddColumn<int>(
                name: "TweetId",
                table: "Tweets",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tweets",
                table: "Tweets",
                column: "TweetId");
        }
    }
}
