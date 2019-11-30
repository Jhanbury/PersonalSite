using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Site.Persistance.Migrations
{
    public partial class updatedblogpostmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBlogPosts",
                table: "UserBlogPosts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserBlogPosts");

            migrationBuilder.AddColumn<int>(
                name: "BlogId",
                table: "UserBlogPosts",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Source",
                table: "UserBlogPosts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SourceId",
                table: "UserBlogPosts",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBlogPosts",
                table: "UserBlogPosts",
                column: "BlogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBlogPosts",
                table: "UserBlogPosts");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "UserBlogPosts");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "UserBlogPosts");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "UserBlogPosts");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "UserBlogPosts",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBlogPosts",
                table: "UserBlogPosts",
                column: "Id");
        }
    }
}
