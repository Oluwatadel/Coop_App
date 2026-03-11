using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoopApplication.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class refix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "transaction_reference_no",
                table: "transaction",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "transaction_reference_no",
                table: "transaction");
        }
    }
}
