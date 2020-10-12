using Microsoft.EntityFrameworkCore.Migrations;

namespace Site.Persistance.Migrations
{
    public partial class addedavatarandreadcolumnstoblog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinutesToRead",
                table: "UserBlogPosts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserAvatar",
                table: "UserBlogPosts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinutesToRead",
                table: "UserBlogPosts");

            migrationBuilder.DropColumn(
                name: "UserAvatar",
                table: "UserBlogPosts");
        }
    }
}
