using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nordax.Bank.Recruitment.DataAccess.Migrations.File
{
    public partial class AddContentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "FileRecords",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "FileRecords");
        }
    }
}
