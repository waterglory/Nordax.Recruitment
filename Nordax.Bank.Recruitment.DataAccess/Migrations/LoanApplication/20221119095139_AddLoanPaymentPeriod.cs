using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nordax.Bank.Recruitment.DataAccess.Migrations.LoanApplication
{
    public partial class AddLoanPaymentPeriod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentPeriod",
                table: "Loans",
                type: "int",
                precision: 4,
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentPeriod",
                table: "Loans");
        }
    }
}
