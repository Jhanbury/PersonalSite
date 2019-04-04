using Microsoft.EntityFrameworkCore.Migrations;

namespace Site.Persistance.Migrations
{
    public partial class removedextrafk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ContactInformations_ContactInformationId1",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ContactInformationId1",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ContactInformationId1",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContactInformationId1",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ContactInformationId1",
                table: "Users",
                column: "ContactInformationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ContactInformations_ContactInformationId1",
                table: "Users",
                column: "ContactInformationId1",
                principalTable: "ContactInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
