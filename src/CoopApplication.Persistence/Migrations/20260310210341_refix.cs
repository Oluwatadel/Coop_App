using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoopApplication.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class refix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_loan_type_name",
                table: "loan_type");

            migrationBuilder.CreateIndex(
                name: "IX_loan_type_name",
                table: "loan_type",
                column: "name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_loan_type_name",
                table: "loan_type");

            migrationBuilder.CreateIndex(
                name: "IX_loan_type_name",
                table: "loan_type",
                column: "name",
                unique: true);
        }
    }
}
