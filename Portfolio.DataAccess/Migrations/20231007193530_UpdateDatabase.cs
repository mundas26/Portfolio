using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_Certifications_CertificationId",
                table: "Educations");

            migrationBuilder.DropIndex(
                name: "IX_Educations_CertificationId",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "CertificationId",
                table: "Educations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CertificationId",
                table: "Educations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Educations_CertificationId",
                table: "Educations",
                column: "CertificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_Certifications_CertificationId",
                table: "Educations",
                column: "CertificationId",
                principalTable: "Certifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
