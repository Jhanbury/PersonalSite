using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Site.Persistance.Migrations
{
    public partial class addedcertificationtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accreditor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accreditor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Certification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    AccreditorId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certification_Accreditor_AccreditorId",
                        column: x => x.AccreditorId,
                        principalTable: "Accreditor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCertification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CertificationId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    DateObtained = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCertification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCertification_Certification_CertificationId",
                        column: x => x.CertificationId,
                        principalTable: "Certification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCertification_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certification_AccreditorId",
                table: "Certification",
                column: "AccreditorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCertification_CertificationId",
                table: "UserCertification",
                column: "CertificationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCertification_UserId",
                table: "UserCertification",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCertification");

            migrationBuilder.DropTable(
                name: "Certification");

            migrationBuilder.DropTable(
                name: "Accreditor");
        }
    }
}
