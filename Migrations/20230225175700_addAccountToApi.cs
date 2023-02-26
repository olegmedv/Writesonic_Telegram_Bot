using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleAppWritesonic.Migrations
{
    /// <inheritdoc />
    public partial class addAccountToApi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "ApiKeys");

            migrationBuilder.AddColumn<string>(
                name: "Account",
                table: "ApiKeys",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Account",
                table: "ApiKeys");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "ApiKeys",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
