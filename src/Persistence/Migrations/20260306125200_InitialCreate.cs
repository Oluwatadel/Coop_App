using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoopApplication.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "user");

            migrationBuilder.AddColumn<Guid>(
                name: "AssociationId",
                table: "user",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "user",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "user",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "first_name",
                table: "user",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "user",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                table: "user",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "phone",
                table: "user",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    total_shares = table.Column<int>(type: "integer", nullable: false),
                    savings_balance = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    total_interest_accrued = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Modifier = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_account_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "association",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    association_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Modifier = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_association", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "loan_type",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    minimum_loan_repayment = table.Column<int>(type: "integer", nullable: false),
                    annual_interest_rate = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    liquidity_period = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Modifier = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loan_type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "loan_taken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    loan_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    principal_amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    total_payable = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    balance_remaining = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Modifier = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loan_taken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_loan_taken_loan_type_loan_type_id",
                        column: x => x.loan_type_id,
                        principalTable: "loan_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_loan_taken_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    loan_id = table.Column<Guid>(type: "uuid", nullable: true),
                    amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    transaction_type = table.Column<int>(type: "integer", nullable: false),
                    payment_method = table.Column<int>(type: "integer", nullable: false),
                    admin_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Modifier = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_transaction_loan_taken_loan_id",
                        column: x => x.loan_id,
                        principalTable: "loan_taken",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_transaction_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_AssociationId",
                table: "user",
                column: "AssociationId");

            migrationBuilder.CreateIndex(
                name: "IX_user_email",
                table: "user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_RoleId",
                table: "user",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_account_user_id",
                table: "account",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_loan_taken_loan_type_id",
                table: "loan_taken",
                column: "loan_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_loan_taken_user_id",
                table: "loan_taken",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_loan_type_name",
                table: "loan_type",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_transaction_loan_id",
                table: "transaction",
                column: "loan_id");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_user_id",
                table: "transaction",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_association_AssociationId",
                table: "user",
                column: "AssociationId",
                principalTable: "association",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_RoleId",
                table: "user",
                column: "RoleId",
                principalTable: "role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_association_AssociationId",
                table: "user");

            migrationBuilder.DropForeignKey(
                name: "FK_user_role_RoleId",
                table: "user");

            migrationBuilder.DropTable(
                name: "account");

            migrationBuilder.DropTable(
                name: "association");

            migrationBuilder.DropTable(
                name: "transaction");

            migrationBuilder.DropTable(
                name: "loan_taken");

            migrationBuilder.DropTable(
                name: "loan_type");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_AssociationId",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_email",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_RoleId",
                table: "user");

            migrationBuilder.DropColumn(
                name: "AssociationId",
                table: "user");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "user");

            migrationBuilder.DropColumn(
                name: "email",
                table: "user");

            migrationBuilder.DropColumn(
                name: "first_name",
                table: "user");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "user");

            migrationBuilder.DropColumn(
                name: "last_name",
                table: "user");

            migrationBuilder.DropColumn(
                name: "phone",
                table: "user");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");
        }
    }
}
