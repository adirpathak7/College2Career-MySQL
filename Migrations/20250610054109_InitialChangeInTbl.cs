using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace College2Career.Migrations
{
    /// <inheritdoc />
    public partial class InitialChangeInTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Interviews_applicationId",
                table: "Interviews");

            migrationBuilder.RenameColumn(
                name: "offerLater",
                table: "Offers",
                newName: "offerLetter");

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_applicationId",
                table: "Interviews",
                column: "applicationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Interviews_applicationId",
                table: "Interviews");

            migrationBuilder.RenameColumn(
                name: "offerLetter",
                table: "Offers",
                newName: "offerLater");

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_applicationId",
                table: "Interviews",
                column: "applicationId",
                unique: true,
                filter: "[applicationId] IS NOT NULL");
        }
    }
}
