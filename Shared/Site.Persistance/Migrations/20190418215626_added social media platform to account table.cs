using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Site.Persistance.Migrations
{
    public partial class addedsocialmediaplatformtoaccounttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "SocialMediaAccounts",
                newName: "SocialMediaPlatformId");

            migrationBuilder.CreateTable(
                name: "SocialMediaPlatform",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMediaPlatform", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SocialMediaAccounts_SocialMediaPlatformId",
                table: "SocialMediaAccounts",
                column: "SocialMediaPlatformId");

            migrationBuilder.AddForeignKey(
                name: "FK_SocialMediaAccounts_SocialMediaPlatform_SocialMediaPlatformId",
                table: "SocialMediaAccounts",
                column: "SocialMediaPlatformId",
                principalTable: "SocialMediaPlatform",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SocialMediaAccounts_SocialMediaPlatform_SocialMediaPlatformId",
                table: "SocialMediaAccounts");

            migrationBuilder.DropTable(
                name: "SocialMediaPlatform");

            migrationBuilder.DropIndex(
                name: "IX_SocialMediaAccounts_SocialMediaPlatformId",
                table: "SocialMediaAccounts");

            migrationBuilder.RenameColumn(
                name: "SocialMediaPlatformId",
                table: "SocialMediaAccounts",
                newName: "Type");
        }
    }
}
