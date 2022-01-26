using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorBob.Core.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "shared");

            migrationBuilder.CreateSequence<int>(
                name: "DrugNr",
                schema: "shared",
                startValue: 1000L);

            migrationBuilder.CreateSequence<int>(
                name: "PatientNr",
                schema: "shared",
                startValue: 10000L);

            migrationBuilder.CreateSequence<int>(
                name: "RobotNr",
                schema: "shared");

            migrationBuilder.CreateSequence<int>(
                name: "StaffNr",
                schema: "shared",
                startValue: 100L);

            migrationBuilder.CreateSequence<int>(
                name: "TherapyNr",
                schema: "shared",
                startValue: 100L);

            migrationBuilder.CreateSequence<int>(
                name: "TimeModelNr",
                schema: "shared",
                startValue: 1000L);

            migrationBuilder.CreateTable(
                name: "Drugs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoseInMg = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drugs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Robot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<int>(type: "int", nullable: false),
                    Power = table.Column<int>(type: "int", nullable: false),
                    Activity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Robot", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaffMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffMembers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Therapies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuantityOfDrug = table.Column<int>(type: "int", nullable: false),
                    DrugId = table.Column<int>(type: "int", nullable: false),
                    CaringStaffId = table.Column<int>(type: "int", nullable: false),
                    TimeModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Therapies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Therapies_Drugs_DrugId",
                        column: x => x.DrugId,
                        principalTable: "Drugs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Therapies_StaffMembers_CaringStaffId",
                        column: x => x.CaringStaffId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Therapies_TimeModels_TimeModelId",
                        column: x => x.TimeModelId,
                        principalTable: "TimeModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    TherapyId = table.Column<int>(type: "int", nullable: false),
                    MedicalHistory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeavingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Therapies_TherapyId",
                        column: x => x.TherapyId,
                        principalTable: "Therapies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Drugs",
                columns: new[] { "Id", "DoseInMg", "Name" },
                values: new object[,]
                {
                    { 1000, 50, "AAA" },
                    { 1001, 250, "BBB" },
                    { 1002, 75, "CCC" },
                    { 1003, 115, "DDD" }
                });

            migrationBuilder.InsertData(
                table: "Robot",
                columns: new[] { "Id", "Activity", "Location", "Name", "Power" },
                values: new object[] { 1, 0, 0, "DoctorBob 1.0", 100 });

            migrationBuilder.InsertData(
                table: "StaffMembers",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Role", "Username" },
                values: new object[,]
                {
                    { 140, "", "Katherina", "Popp", 4, "kpopp" },
                    { 130, "", "Walter", "Seger", 3, "wseger" },
                    { 123, "", "Sybille", "Fernandez", 0, "sfernandez" },
                    { 122, "", "Roland", "Agger", 0, "ragger" },
                    { 120, "", "Selina", "Kluser", 0, "skluser" },
                    { 102, "", "Ibrahim", "Kesay", 1, "ikesay" },
                    { 101, "", "Rudolf", "Sahli", 1, "rsahli" },
                    { 100, "", "Angela", "Schmitter", 2, "aschmitter" },
                    { 121, "", "Jacqueline", "Seitz", 0, "sseitz" }
                });

            migrationBuilder.InsertData(
                table: "TimeModels",
                columns: new[] { "Id", "Time" },
                values: new object[,]
                {
                    { 1002, "08.00 / 14.30 / 18.30" },
                    { 1000, "08.30 / 11.00" },
                    { 1001, "11.00" },
                    { 1003, "18.00" }
                });

            migrationBuilder.InsertData(
                table: "Therapies",
                columns: new[] { "Id", "CaringStaffId", "DrugId", "Name", "QuantityOfDrug", "TimeModelId" },
                values: new object[,]
                {
                    { 103, 122, 1003, "DDD 2x täglich, 115mg", 2, 1000 },
                    { 100, 120, 1000, "AAA 1x täglich, 50mg", 1, 1001 },
                    { 101, 121, 1001, "BBB 3x täglich, 250mg", 3, 1002 },
                    { 102, 123, 1002, "CCC 1x täglich, 75mg", 1, 1003 }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "EntryDate", "FirstName", "Gender", "LastName", "LeavingDate", "MedicalHistory", "TherapyId" },
                values: new object[,]
                {
                    { 10000, new DateTime(2022, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marco", 0, "Inverardi", new DateTime(2022, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Operation Blinddarum im Jahre 2018", 103 },
                    { 10002, new DateTime(2022, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Antonio", 0, "Eichholzer", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 103 },
                    { 10003, new DateTime(2022, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Martha", 1, "Watson", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Schlaganfall im Juli 2015, diverse tägliche Medikamentenzunahme", 100 },
                    { 10005, new DateTime(2022, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lisa", 1, "Zellweger", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Herzstillstand 05.2011 mit anschliessender Reanimation", 101 },
                    { 10001, new DateTime(2022, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Heidi", 1, "Geissbühler", new DateTime(2022, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hüftoperation 01.2017", 102 },
                    { 10004, new DateTime(2022, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Flavio", 0, "Frei", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 102 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_TherapyId",
                table: "Patients",
                column: "TherapyId");

            migrationBuilder.CreateIndex(
                name: "IX_Therapies_CaringStaffId",
                table: "Therapies",
                column: "CaringStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Therapies_DrugId",
                table: "Therapies",
                column: "DrugId");

            migrationBuilder.CreateIndex(
                name: "IX_Therapies_TimeModelId",
                table: "Therapies",
                column: "TimeModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Robot");

            migrationBuilder.DropTable(
                name: "Therapies");

            migrationBuilder.DropTable(
                name: "Drugs");

            migrationBuilder.DropTable(
                name: "StaffMembers");

            migrationBuilder.DropTable(
                name: "TimeModels");

            migrationBuilder.DropSequence(
                name: "DrugNr",
                schema: "shared");

            migrationBuilder.DropSequence(
                name: "PatientNr",
                schema: "shared");

            migrationBuilder.DropSequence(
                name: "RobotNr",
                schema: "shared");

            migrationBuilder.DropSequence(
                name: "StaffNr",
                schema: "shared");

            migrationBuilder.DropSequence(
                name: "TherapyNr",
                schema: "shared");

            migrationBuilder.DropSequence(
                name: "TimeModelNr",
                schema: "shared");
        }
    }
}
