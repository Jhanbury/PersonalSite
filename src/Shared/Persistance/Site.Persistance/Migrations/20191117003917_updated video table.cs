using Microsoft.EntityFrameworkCore.Migrations;

namespace Site.Persistance.Migrations
{
    public partial class updatedvideotable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SourceId",
                table: "Videos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoDuration",
                table: "Videos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "VideoDuration",
                table: "Videos");
        }
    }
}
