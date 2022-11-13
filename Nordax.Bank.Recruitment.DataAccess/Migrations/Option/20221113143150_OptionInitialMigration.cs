using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nordax.Bank.Recruitment.DataAccess.Migrations.Option
{
    public partial class OptionInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BindingPeriods",
                columns: table => new
                {
                    Length = table.Column<int>(type: "int", precision: 3, nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(8,5)", precision: 8, scale: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BindingPeriods", x => x.Length);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BindingPeriods");
        }
    }
}
