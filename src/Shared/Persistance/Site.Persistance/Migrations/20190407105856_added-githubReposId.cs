using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Site.Persistance.Migrations
{
    public partial class addedgithubReposId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GithubRepos",
                table: "GithubRepos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GithubRepos");

            migrationBuilder.AddColumn<int>(
                name: "RepoId",
                table: "GithubRepos",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<long>(
                name: "GithubId",
                table: "GithubRepos",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GithubRepos",
                table: "GithubRepos",
                column: "RepoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GithubRepos",
                table: "GithubRepos");

            migrationBuilder.DropColumn(
                name: "RepoId",
                table: "GithubRepos");

            migrationBuilder.DropColumn(
                name: "GithubId",
                table: "GithubRepos");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "GithubRepos",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GithubRepos",
                table: "GithubRepos",
                column: "Id");
        }
    }
}
