using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaseManagmentAPI.Migrations
{
    public partial class CMCaseState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "state",
                table: "CMCase",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "state",
                table: "CMCase");
        }
    }
}
