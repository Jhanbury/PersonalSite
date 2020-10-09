using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Site.Persistance.Migrations
{
    public partial class addedtagstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_ProjectSkills_Projects_ProjectId",
            //    table: "ProjectSkills");

            migrationBuilder.AddColumn<int>(
                name: "Comments",
                table: "UserBlogPosts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "UserBlogPosts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishDate",
                table: "UserBlogPosts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "UserBlogPosts",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.AddColumn<int>(
            //    name: "ProjectId2",
            //    table: "ProjectSkills",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BlogPostTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tag = table.Column<string>(nullable: false),
                    UserBlogPostId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogPostTags_UserBlogPosts_UserBlogPostId",
                        column: x => x.UserBlogPostId,
                        principalTable: "UserBlogPosts",
                        principalColumn: "BlogId",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProjectSkills_ProjectId2",
            //    table: "ProjectSkills",
            //    column: "ProjectId2");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostTags_UserBlogPostId",
                table: "BlogPostTags",
                column: "UserBlogPostId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ProjectSkills_Projects_ProjectId2",
            //    table: "ProjectSkills",
            //    column: "ProjectId2",
            //    principalTable: "Projects",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_ProjectSkills_Projects_ProjectId2",
            //    table: "ProjectSkills");

            migrationBuilder.DropTable(
                name: "BlogPostTags");

            //migrationBuilder.DropIndex(
            //    name: "IX_ProjectSkills_ProjectId2",
            //    table: "ProjectSkills");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "UserBlogPosts");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "UserBlogPosts");

            migrationBuilder.DropColumn(
                name: "PublishDate",
                table: "UserBlogPosts");

            migrationBuilder.DropColumn(
                name: "Views",
                table: "UserBlogPosts");

            migrationBuilder.DropColumn(
                name: "ProjectId2",
                table: "ProjectSkills");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ProjectSkills_Projects_ProjectId",
            //    table: "ProjectSkills",
            //    column: "ProjectId",
            //    principalTable: "Projects",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
