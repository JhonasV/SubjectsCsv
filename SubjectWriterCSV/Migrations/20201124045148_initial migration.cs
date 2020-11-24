using Microsoft.EntityFrameworkCore.Migrations;

namespace SubjectWriterCSV.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Credits = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    SchoolRoom = table.Column<string>(maxLength: 50, nullable: false),
                    Schedule = table.Column<string>(maxLength: 50, nullable: false),
                    Quater = table.Column<string>(maxLength: 50, nullable: false),
                    MinLiteralForApprove = table.Column<string>(maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
