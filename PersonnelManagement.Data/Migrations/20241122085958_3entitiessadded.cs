using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonnelManagement.Data.Migrations
{
    public partial class _3entitiessadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "MIS");

            migrationBuilder.CreateTable(
                name: "FieldDefinition",
                schema: "MIS",
                columns: table => new
                {
                    pk_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FieldType = table.Column<int>(type: "int", nullable: true),
                    IsRequired = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldDefinition", x => x.pk_Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonInfo",
                schema: "MIS",
                columns: table => new
                {
                    pk_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonnelCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonInfo", x => x.pk_Id);
                });

            migrationBuilder.CreateTable(
                name: "FieldSubmission",
                schema: "MIS",
                columns: table => new
                {
                    pk_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fk_FieldDefinition = table.Column<long>(type: "bigint", nullable: false),
                    fk_PersonInfo = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldSubmission", x => x.pk_Id);
                    table.ForeignKey(
                        name: "FK_FieldSubmission_FieldDefinition_fk_FieldDefinition",
                        column: x => x.fk_FieldDefinition,
                        principalSchema: "MIS",
                        principalTable: "FieldDefinition",
                        principalColumn: "pk_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FieldSubmission_PersonInfo_fk_PersonInfo",
                        column: x => x.fk_PersonInfo,
                        principalSchema: "MIS",
                        principalTable: "PersonInfo",
                        principalColumn: "pk_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FieldSubmission_fk_FieldDefinition",
                schema: "MIS",
                table: "FieldSubmission",
                column: "fk_FieldDefinition");

            migrationBuilder.CreateIndex(
                name: "IX_FieldSubmission_fk_PersonInfo",
                schema: "MIS",
                table: "FieldSubmission",
                column: "fk_PersonInfo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FieldSubmission",
                schema: "MIS");

            migrationBuilder.DropTable(
                name: "FieldDefinition",
                schema: "MIS");

            migrationBuilder.DropTable(
                name: "PersonInfo",
                schema: "MIS");
        }
    }
}
