using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace College2Career.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colleges",
                columns: table => new
                {
                    collegeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    collegeName = table.Column<string>(type: "varchar(50)", nullable: true),
                    establishedDate = table.Column<string>(type: "varchar(50)", nullable: true),
                    contactNumber = table.Column<string>(type: "varchar(50)", nullable: true),
                    profilePicture = table.Column<string>(type: "varchar(500)", nullable: true),
                    area = table.Column<string>(type: "varchar(50)", nullable: true),
                    city = table.Column<string>(type: "varchar(50)", nullable: true),
                    state = table.Column<string>(type: "varchar(50)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colleges", x => x.collegeId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    roleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.roleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    usersId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "varchar(50)", nullable: true),
                    password = table.Column<string>(type: "varchar(255)", nullable: true),
                    forgotPasswordToken = table.Column<string>(type: "varchar(255)", nullable: true),
                    tokenExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    roleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.usersId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_roleId",
                        column: x => x.roleId,
                        principalTable: "Roles",
                        principalColumn: "roleId");
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    companyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    companyName = table.Column<string>(type: "varchar(50)", nullable: true),
                    establishedDate = table.Column<string>(type: "varchar(50)", nullable: true),
                    contactNumber = table.Column<string>(type: "varchar(50)", nullable: true),
                    profilePicture = table.Column<string>(type: "varchar(500)", nullable: true),
                    industry = table.Column<string>(type: "varchar(50)", nullable: true),
                    area = table.Column<string>(type: "varchar(50)", nullable: true),
                    city = table.Column<string>(type: "varchar(50)", nullable: true),
                    state = table.Column<string>(type: "varchar(50)", nullable: true),
                    employeeSize = table.Column<string>(type: "varchar(10)", nullable: true),
                    reasonOfStatus = table.Column<string>(type: "varchar(100)", nullable: true),
                    status = table.Column<string>(type: "varchar(20)", nullable: true),
                    usersId = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.companyId);
                    table.ForeignKey(
                        name: "FK_Companies_Users_usersId",
                        column: x => x.usersId,
                        principalTable: "Users",
                        principalColumn: "usersId");
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    studentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    studentName = table.Column<string>(type: "varchar(50)", nullable: true),
                    rollNumber = table.Column<string>(type: "varchar(15)", nullable: true),
                    course = table.Column<string>(type: "varchar(25)", nullable: true),
                    graduationYear = table.Column<string>(type: "varchar(15)", nullable: true),
                    resume = table.Column<string>(type: "varchar(500)", nullable: true),
                    verified = table.Column<string>(type: "varchar(15)", nullable: true),
                    userId = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.studentId);
                    table.ForeignKey(
                        name: "FK_Students_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "usersId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vacancies",
                columns: table => new
                {
                    vacancyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "varchar(50)", nullable: true),
                    description = table.Column<string>(type: "varchar(500)", nullable: true),
                    eligibility_criteria = table.Column<string>(type: "varchar(200)", nullable: true),
                    totalVacancy = table.Column<string>(type: "varchar(10)", nullable: true),
                    timing = table.Column<string>(type: "varchar(50)", nullable: true),
                    package = table.Column<string>(type: "varchar(50)", nullable: true),
                    type = table.Column<string>(type: "varchar(15)", nullable: true),
                    locationType = table.Column<string>(type: "varchar(15)", nullable: true),
                    status = table.Column<string>(type: "varchar(15)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    companyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacancies", x => x.vacancyId);
                    table.ForeignKey(
                        name: "FK_Vacancies_Companies_companyId",
                        column: x => x.companyId,
                        principalTable: "Companies",
                        principalColumn: "companyId");
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    feedbackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    comments = table.Column<string>(type: "varchar(500)", nullable: true),
                    rating = table.Column<string>(type: "varchar(10)", nullable: true),
                    studentId = table.Column<int>(type: "int", nullable: true),
                    companyId = table.Column<int>(type: "int", nullable: true),
                    collegeId = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.feedbackId);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Colleges_collegeId",
                        column: x => x.collegeId,
                        principalTable: "Colleges",
                        principalColumn: "collegeId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Companies_companyId",
                        column: x => x.companyId,
                        principalTable: "Companies",
                        principalColumn: "companyId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Students_studentId",
                        column: x => x.studentId,
                        principalTable: "Students",
                        principalColumn: "studentId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    applicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reason = table.Column<string>(type: "varchar(300)", nullable: true),
                    status = table.Column<string>(type: "varchar(15)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    studentId = table.Column<int>(type: "int", nullable: true),
                    vacancyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.applicationId);
                    table.ForeignKey(
                        name: "FK_Applications_Students_studentId",
                        column: x => x.studentId,
                        principalTable: "Students",
                        principalColumn: "studentId");
                    table.ForeignKey(
                        name: "FK_Applications_Vacancies_vacancyId",
                        column: x => x.vacancyId,
                        principalTable: "Vacancies",
                        principalColumn: "vacancyId");
                });

            migrationBuilder.CreateTable(
                name: "Placements",
                columns: table => new
                {
                    placementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    placementDate = table.Column<string>(type: "varchar(50)", nullable: true),
                    salaryPackage = table.Column<string>(type: "varchar(10)", nullable: true),
                    studentId = table.Column<int>(type: "int", nullable: true),
                    companyId = table.Column<int>(type: "int", nullable: true),
                    collegeId = table.Column<int>(type: "int", nullable: true),
                    vacancyId = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Placements", x => x.placementId);
                    table.ForeignKey(
                        name: "FK_Placements_Colleges_collegeId",
                        column: x => x.collegeId,
                        principalTable: "Colleges",
                        principalColumn: "collegeId");
                    table.ForeignKey(
                        name: "FK_Placements_Companies_companyId",
                        column: x => x.companyId,
                        principalTable: "Companies",
                        principalColumn: "companyId");
                    table.ForeignKey(
                        name: "FK_Placements_Students_studentId",
                        column: x => x.studentId,
                        principalTable: "Students",
                        principalColumn: "studentId");
                    table.ForeignKey(
                        name: "FK_Placements_Vacancies_vacancyId",
                        column: x => x.vacancyId,
                        principalTable: "Vacancies",
                        principalColumn: "vacancyId");
                });

            migrationBuilder.CreateTable(
                name: "Interviews",
                columns: table => new
                {
                    interviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    interviewDate = table.Column<string>(type: "varchar(50)", nullable: true),
                    interviewTime = table.Column<string>(type: "varchar(50)", nullable: true),
                    status = table.Column<string>(type: "varchar(15)", nullable: true),
                    applicationId = table.Column<int>(type: "int", nullable: true),
                    collegeId = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interviews", x => x.interviewId);
                    table.ForeignKey(
                        name: "FK_Interviews_Applications_applicationId",
                        column: x => x.applicationId,
                        principalTable: "Applications",
                        principalColumn: "applicationId");
                    table.ForeignKey(
                        name: "FK_Interviews_Colleges_collegeId",
                        column: x => x.collegeId,
                        principalTable: "Colleges",
                        principalColumn: "collegeId");
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    offerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    offerLater = table.Column<string>(type: "varchar(500)", nullable: true),
                    status = table.Column<string>(type: "varchar(15)", nullable: true),
                    reason = table.Column<string>(type: "varchar(255)", nullable: true),
                    applicationId = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.offerId);
                    table.ForeignKey(
                        name: "FK_Offers_Applications_applicationId",
                        column: x => x.applicationId,
                        principalTable: "Applications",
                        principalColumn: "applicationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_studentId",
                table: "Applications",
                column: "studentId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_vacancyId",
                table: "Applications",
                column: "vacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_usersId",
                table: "Companies",
                column: "usersId",
                unique: true,
                filter: "[usersId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_collegeId",
                table: "Feedbacks",
                column: "collegeId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_companyId",
                table: "Feedbacks",
                column: "companyId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_studentId",
                table: "Feedbacks",
                column: "studentId");

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_applicationId",
                table: "Interviews",
                column: "applicationId",
                unique: true,
                filter: "[applicationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_collegeId",
                table: "Interviews",
                column: "collegeId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_applicationId",
                table: "Offers",
                column: "applicationId",
                unique: true,
                filter: "[applicationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Placements_collegeId",
                table: "Placements",
                column: "collegeId");

            migrationBuilder.CreateIndex(
                name: "IX_Placements_companyId",
                table: "Placements",
                column: "companyId");

            migrationBuilder.CreateIndex(
                name: "IX_Placements_studentId",
                table: "Placements",
                column: "studentId");

            migrationBuilder.CreateIndex(
                name: "IX_Placements_vacancyId",
                table: "Placements",
                column: "vacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_userId",
                table: "Students",
                column: "userId",
                unique: true,
                filter: "[userId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_roleId",
                table: "Users",
                column: "roleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_companyId",
                table: "Vacancies",
                column: "companyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Interviews");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Placements");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Colleges");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Vacancies");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
