using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestWork.Migrations
{
    public partial class UpdateEmailLogcs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DisplayMessage",
                table: "EmailLogs",
                newName: "FailedMessage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FailedMessage",
                table: "EmailLogs",
                newName: "DisplayMessage");
        }
    }
}
