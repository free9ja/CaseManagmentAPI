using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaseManagmentAPI.Migrations
{
    public partial class CMCase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CMCase_CMCustomerCare_customerCareID",
                table: "CMCase");

            migrationBuilder.DropColumn(
                name: "agentId",
                table: "CMCase");

            migrationBuilder.RenameColumn(
                name: "customerCareID",
                table: "CMCase",
                newName: "customerCareId");

            migrationBuilder.RenameIndex(
                name: "IX_CMCase_customerCareID",
                table: "CMCase",
                newName: "IX_CMCase_customerCareId");

            migrationBuilder.AlterColumn<int>(
                name: "customerCareId",
                table: "CMCase",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_CMCase_CMCustomerCare_customerCareId",
                table: "CMCase",
                column: "customerCareId",
                principalTable: "CMCustomerCare",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CMCase_CMCustomerCare_customerCareId",
                table: "CMCase");

            migrationBuilder.RenameColumn(
                name: "customerCareId",
                table: "CMCase",
                newName: "customerCareID");

            migrationBuilder.RenameIndex(
                name: "IX_CMCase_customerCareId",
                table: "CMCase",
                newName: "IX_CMCase_customerCareID");

            migrationBuilder.AlterColumn<int>(
                name: "customerCareID",
                table: "CMCase",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "agentId",
                table: "CMCase",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_CMCase_CMCustomerCare_customerCareID",
                table: "CMCase",
                column: "customerCareID",
                principalTable: "CMCustomerCare",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
