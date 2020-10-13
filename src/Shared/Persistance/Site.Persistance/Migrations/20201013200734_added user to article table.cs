using Microsoft.EntityFrameworkCore.Migrations;

namespace Site.Persistance.Migrations
{
    public partial class addedusertoarticletable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Article",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Article_UserId",
                table: "Article",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Users_UserId",
                table: "Article",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_Users_UserId",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_UserId",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Article");
        }
    }
}
