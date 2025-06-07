using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace College2Career.Migrations
{
    /// <inheritdoc />
    public partial class ChangedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_Colleges_collegeId",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "area",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "area",
                table: "Colleges");

            migrationBuilder.RenameColumn(
                name: "package",
                table: "Vacancies",
                newName: "annualPackage");

            migrationBuilder.RenameColumn(
                name: "collegeId",
                table: "Interviews",
                newName: "CollegescollegeId");

            migrationBuilder.RenameIndex(
                name: "IX_Interviews_collegeId",
                table: "Interviews",
                newName: "IX_Interviews_CollegescollegeId");

            migrationBuilder.AlterColumn<string>(
                name: "eligibility_criteria",
                table: "Vacancies",
                type: "varchar(3000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "Vacancies",
                type: "varchar(3000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedAt",
                table: "Vacancies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "forgotPasswordToken",
                table: "Users",
                type: "varchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "statusReason",
                table: "Students",
                type: "varchar(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "resume",
                table: "Students",
                type: "varchar(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "reason",
                table: "Offers",
                type: "varchar(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "offerLater",
                table: "Offers",
                type: "varchar(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "reason",
                table: "Interviews",
                type: "varchar(1000)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedAt",
                table: "Interviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "comments",
                table: "Feedbacks",
                type: "varchar(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "reasonOfStatus",
                table: "Companies",
                type: "varchar(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "profilePicture",
                table: "Companies",
                type: "varchar(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "Companies",
                type: "varchar(350)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "profilePicture",
                table: "Colleges",
                type: "varchar(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "collegeName",
                table: "Colleges",
                type: "varchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "Colleges",
                type: "varchar(350)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "Applications",
                type: "varchar(25)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "reason",
                table: "Applications",
                type: "varchar(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(300)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedAt",
                table: "Applications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_Colleges_CollegescollegeId",
                table: "Interviews",
                column: "CollegescollegeId",
                principalTable: "Colleges",
                principalColumn: "collegeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_Colleges_CollegescollegeId",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "updatedAt",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "reason",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "updatedAt",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "address",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "address",
                table: "Colleges");

            migrationBuilder.DropColumn(
                name: "updatedAt",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "annualPackage",
                table: "Vacancies",
                newName: "package");

            migrationBuilder.RenameColumn(
                name: "CollegescollegeId",
                table: "Interviews",
                newName: "collegeId");

            migrationBuilder.RenameIndex(
                name: "IX_Interviews_CollegescollegeId",
                table: "Interviews",
                newName: "IX_Interviews_collegeId");

            migrationBuilder.AlterColumn<string>(
                name: "eligibility_criteria",
                table: "Vacancies",
                type: "varchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(3000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "Vacancies",
                type: "varchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(3000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "forgotPasswordToken",
                table: "Users",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "statusReason",
                table: "Students",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "resume",
                table: "Students",
                type: "varchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "reason",
                table: "Offers",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "offerLater",
                table: "Offers",
                type: "varchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "comments",
                table: "Feedbacks",
                type: "varchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "reasonOfStatus",
                table: "Companies",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "profilePicture",
                table: "Companies",
                type: "varchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "area",
                table: "Companies",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "profilePicture",
                table: "Colleges",
                type: "varchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "collegeName",
                table: "Colleges",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "area",
                table: "Colleges",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "Applications",
                type: "varchar(15)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "reason",
                table: "Applications",
                type: "varchar(300)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_Colleges_collegeId",
                table: "Interviews",
                column: "collegeId",
                principalTable: "Colleges",
                principalColumn: "collegeId");
        }
    }
}
