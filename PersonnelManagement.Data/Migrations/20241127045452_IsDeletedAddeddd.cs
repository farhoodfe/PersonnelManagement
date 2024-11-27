using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonnelManagement.Data.Migrations
{
    public partial class IsDeletedAddeddd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldSubmission_DynamicFieldDefinition_fk_FieldDefinition",
                schema: "MIS",
                table: "FieldSubmission");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "MIS",
                table: "PersonInfo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<long>(
                name: "fk_FieldDefinition",
                schema: "MIS",
                table: "FieldSubmission",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "MIS",
                table: "FieldSubmission",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_FieldSubmission_DynamicFieldDefinition_fk_FieldDefinition",
                schema: "MIS",
                table: "FieldSubmission",
                column: "fk_FieldDefinition",
                principalSchema: "MIS",
                principalTable: "DynamicFieldDefinition",
                principalColumn: "pk_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldSubmission_DynamicFieldDefinition_fk_FieldDefinition",
                schema: "MIS",
                table: "FieldSubmission");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "MIS",
                table: "PersonInfo");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "MIS",
                table: "FieldSubmission");

            migrationBuilder.AlterColumn<long>(
                name: "fk_FieldDefinition",
                schema: "MIS",
                table: "FieldSubmission",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

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
    }
}
