using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleFinance.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_Account_Iban_SwiftBic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Iban",
                table: "Accounts",
                type: "character varying(34)",
                maxLength: 34,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SwiftBic",
                table: "Accounts",
                type: "character varying(11)",
                maxLength: 11,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Iban",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "SwiftBic",
                table: "Accounts");
        }
    }
}
