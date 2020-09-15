using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Site.Persistance.Migrations
{
    public partial class addedvideosandplatformaccounttables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlatformAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    IconUrl = table.Column<string>(nullable: true),
                    Platform = table.Column<int>(nullable: false),
                    PlatformId = table.Column<string>(nullable: true),
                    IsLive = table.Column<bool>(nullable: false),
                    Followers = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlatformAccounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    ThumbnailUrl = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    ViewCount = table.Column<int>(nullable: false),
                    PublishDate = table.Column<DateTime>(nullable: false),
                    PlatformAccountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videos_PlatformAccounts_PlatformAccountId",
                        column: x => x.PlatformAccountId,
                        principalTable: "PlatformAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlatformAccounts_UserId",
                table: "PlatformAccounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_PlatformAccountId",
                table: "Videos",
                column: "PlatformAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "PlatformAccounts");
        }
    }
}
