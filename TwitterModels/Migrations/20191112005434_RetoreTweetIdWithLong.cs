using Microsoft.EntityFrameworkCore.Migrations;

namespace TweeterSearchApp.Migrations
{
    public partial class RetoreTweetIdWithLong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tweets",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Tweets");

            migrationBuilder.AddColumn<long>(
                name: "TweetId",
                table: "Tweets",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tweets",
                table: "Tweets",
                column: "TweetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tweets",
                table: "Tweets",
                column: "Id");
        }
    }
}
