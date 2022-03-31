using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevJobs.API.Persistence.Migrations
{
    public partial class AddJobExperience : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobExperience",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Range = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdJobVacancy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobExperience", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobExperience_JobVacancies_IdJobVacancy",
                        column: x => x.IdJobVacancy,
                        principalTable: "JobVacancies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobExperience_IdJobVacancy",
                table: "JobExperience",
                column: "IdJobVacancy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobExperience");
        }
    }
}
