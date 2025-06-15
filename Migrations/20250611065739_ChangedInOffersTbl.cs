using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace College2Career.Migrations
{
    /// <inheritdoc />
    public partial class ChangedInOffersTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_Colleges_CollegescollegeId",
                table: "Interviews");

            migrationBuilder.DropIndex(
                name: "IX_Interviews_CollegescollegeId",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "CollegescollegeId",
                table: "Interviews");

            migrationBuilder.AlterColumn<string>(
                name: "offerLetter",
                table: "Offers",
                type: "varchar(4000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "annualPackage",
                table: "Offers",
                type: "varchar(25)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Offers",
                type: "varchar(2000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "joiningDate",
                table: "Offers",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "position",
                table: "Offers",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "timing",
                table: "Offers",
                type: "varchar(50)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "annualPackage",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "joiningDate",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "position",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "timing",
                table: "Offers");

            migrationBuilder.AlterColumn<string>(
                name: "offerLetter",
                table: "Offers",
                type: "varchar(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(4000)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollegescollegeId",
                table: "Interviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_CollegescollegeId",
                table: "Interviews",
                column: "CollegescollegeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_Colleges_CollegescollegeId",
                table: "Interviews",
                column: "CollegescollegeId",
                principalTable: "Colleges",
                principalColumn: "collegeId");
        }
    }
}
