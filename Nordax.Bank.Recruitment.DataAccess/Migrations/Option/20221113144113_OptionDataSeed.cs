using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nordax.Bank.Recruitment.DataAccess.Migrations.Option
{
    public partial class OptionDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BindingPeriods",
                columns: new[] { "Length", "InterestRate" },
                values: new object[,]
                {
                    { 1, 2m },
                    { 3, 2.3m },
                    { 6, 2.8m },
                    { 12, 3.3m },
                    { 24, 4m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BindingPeriods",
                keyColumn: "Length",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BindingPeriods",
                keyColumn: "Length",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BindingPeriods",
                keyColumn: "Length",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "BindingPeriods",
                keyColumn: "Length",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "BindingPeriods",
                keyColumn: "Length",
                keyValue: 24);
        }
    }
}
