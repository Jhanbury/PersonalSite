using Microsoft.EntityFrameworkCore.Migrations;

namespace Site.Persistance.Migrations
{
    public partial class updatedproject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectTypeId",
                table: "Projects");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectTypeId",
                table: "Projects",
                nullable: false,
                defaultValue: 0);
        }
    }
}
