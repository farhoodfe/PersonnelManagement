using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonnelManagement.Data.Migrations
{
    public partial class FormulaTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Formula",
                schema: "MIS",
                columns: table => new
                {
                    pk_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormulaName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormulaText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formula", x => x.pk_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Formula",
                schema: "MIS");
        }
    }
}
