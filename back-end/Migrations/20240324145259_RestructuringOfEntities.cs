using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateForProfessor.Migrations
{
    public partial class RestructuringOfEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_DepartmentProfessors_DepartmentProfessorEntityDepartmentId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Profesors_DepartmentProfessors_DepartmentProfessorEntityDepartmentId",
                table: "Profesors");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "ContactNumbers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfessorCourses",
                table: "ProfessorCourses");

            migrationBuilder.DropIndex(
                name: "IX_ProfessorCourses_ProfessorId",
                table: "ProfessorCourses");

            migrationBuilder.DropIndex(
                name: "IX_Profesors_DepartmentProfessorEntityDepartmentId",
                table: "Profesors");

            migrationBuilder.DropIndex(
                name: "IX_Departments_DepartmentProfessorEntityDepartmentId",
                table: "Departments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentProfessors",
                table: "DepartmentProfessors");

            migrationBuilder.DropColumn(
                name: "ProfessorCourseId",
                table: "ProfessorCourses");

            migrationBuilder.DropColumn(
                name: "DepartmentProfessorEntityDepartmentId",
                table: "Profesors");

            migrationBuilder.DropColumn(
                name: "DepartmentProfessorEntityDepartmentId",
                table: "Departments");

            migrationBuilder.AlterColumn<int>(
                name: "ProfessorId",
                table: "ProfessorCourses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "ProfessorCourses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "ProfessorId",
                table: "DepartmentProfessors",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "DepartmentProfessors",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfessorCourses",
                table: "ProfessorCourses",
                columns: new[] { "ProfessorId", "CourseId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentProfessors",
                table: "DepartmentProfessors",
                columns: new[] { "DepartmentId", "ProfessorId" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f4fc0c06-d22a-453b-bd4c-86951fc36a52");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "e5549c67-509d-4239-9cbb-d34b889e1b74");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfessorCourses",
                table: "ProfessorCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentProfessors",
                table: "DepartmentProfessors");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "ProfessorCourses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "ProfessorId",
                table: "ProfessorCourses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<int>(
                name: "ProfessorCourseId",
                table: "ProfessorCourses",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentProfessorEntityDepartmentId",
                table: "Profesors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentProfessorEntityDepartmentId",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProfessorId",
                table: "DepartmentProfessors",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 2)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "DepartmentProfessors",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1)
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfessorCourses",
                table: "ProfessorCourses",
                column: "ProfessorCourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentProfessors",
                table: "DepartmentProfessors",
                column: "DepartmentId");

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniversityId = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZIPCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_Universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Universities",
                        principalColumn: "UniversityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactNumbers",
                columns: table => new
                {
                    ContactNumberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniversityId = table.Column<int>(type: "int", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactNumbers", x => x.ContactNumberId);
                    table.ForeignKey(
                        name: "FK_ContactNumbers_Universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Universities",
                        principalColumn: "UniversityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "e09d7c4b-359f-4611-ac67-30cf9c4e1a15");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "0ca26773-e648-492e-a787-f849d543084f");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorCourses_ProfessorId",
                table: "ProfessorCourses",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Profesors_DepartmentProfessorEntityDepartmentId",
                table: "Profesors",
                column: "DepartmentProfessorEntityDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_DepartmentProfessorEntityDepartmentId",
                table: "Departments",
                column: "DepartmentProfessorEntityDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UniversityId",
                table: "Addresses",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactNumbers_UniversityId",
                table: "ContactNumbers",
                column: "UniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_DepartmentProfessors_DepartmentProfessorEntityDepartmentId",
                table: "Departments",
                column: "DepartmentProfessorEntityDepartmentId",
                principalTable: "DepartmentProfessors",
                principalColumn: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profesors_DepartmentProfessors_DepartmentProfessorEntityDepartmentId",
                table: "Profesors",
                column: "DepartmentProfessorEntityDepartmentId",
                principalTable: "DepartmentProfessors",
                principalColumn: "DepartmentId");
        }
    }
}
