using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonnelManagement.Data.Migrations
{
    public partial class fieldNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldSubmission_FieldDefinition_fk_FieldDefinition",
                schema: "MIS",
                table: "FieldSubmission");

            migrationBuilder.DropTable(
                name: "FieldDefinition",
                schema: "MIS");

            migrationBuilder.CreateTable(
                name: "DynamicFieldDefinition",
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
                    table.PrimaryKey("PK_DynamicFieldDefinition", x => x.pk_Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_FieldSubmission_DynamicFieldDefinition_fk_FieldDefinition",
                schema: "MIS",
                table: "FieldSubmission",
                column: "fk_FieldDefinition",
                principalSchema: "MIS",
                principalTable: "DynamicFieldDefinition",
                principalColumn: "pk_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldSubmission_DynamicFieldDefinition_fk_FieldDefinition",
                schema: "MIS",
                table: "FieldSubmission");

            migrationBuilder.DropTable(
                name: "DynamicFieldDefinition",
                schema: "MIS");

            migrationBuilder.CreateTable(
                name: "FieldDefinition",
                schema: "MIS",
                columns: table => new
                {
                    pk_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRequired = table.Column<bool>(type: "bit", nullable: true),
                    FieldType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldDefinition", x => x.pk_Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_FieldSubmission_FieldDefinition_fk_FieldDefinition",
                schema: "MIS",
                table: "FieldSubmission",
                column: "fk_FieldDefinition",
                principalSchema: "MIS",
                principalTable: "FieldDefinition",
                principalColumn: "pk_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
