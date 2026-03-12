using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoopApplication.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class refix_loan_type2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    total_shares = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    savings_balance = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    total_interest_accrued = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "association",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    association_name = table.Column<string>(type: "varchar(100)", nullable: false),
                    association_description = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Modifier = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_association", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "loan_taken",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    loan_type = table.Column<string>(type: "jsonb", nullable: false),
                    principal_amount = table.Column<decimal>(type: "numeric(18,5)", nullable: false),
                    total_repayment_amount = table.Column<decimal>(type: "numeric(18,5)", nullable: false),
                    MonthlyPaymentAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    balance_remaining = table.Column<decimal>(type: "numeric(18,5)", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    loan_repayments = table.Column<string>(type: "jsonb", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loan_taken", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "loan_type",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    loan_description = table.Column<string>(type: "text", nullable: false),
                    max_loan_amount = table.Column<decimal>(type: "numeric(18,5)", nullable: false),
                    min_loan_amount = table.Column<decimal>(type: "numeric(18,5)", nullable: false),
                    minimum_loan_repayment = table.Column<decimal>(type: "numeric", nullable: false),
                    annual_interest_rate = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    liquidity_period = table.Column<int>(type: "integer", nullable: false),
                    loan_version = table.Column<string>(type: "varchar(50)", nullable: false),
                    previous_loan_type = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Modifier = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loan_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "loanRepayment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    loan_id = table.Column<Guid>(type: "uuid", nullable: false),
                    transaction_id = table.Column<Guid>(type: "uuid", nullable: false),
                    loan_repayment_amount = table.Column<decimal>(type: "numeric(18,5)", nullable: false),
                    payment_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loanRepayment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_name = table.Column<string>(type: "varchar(50)", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "transaction",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    transaction_type = table.Column<string>(type: "text", nullable: false),
                    payment_method = table.Column<string>(type: "text", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaction", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    association_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "varchar(50)", nullable: false),
                    last_name = table.Column<string>(type: "varchar(50)", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", nullable: false),
                    phone = table.Column<string>(type: "varchar(20)", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    loan_taken_id = table.Column<string>(type: "text", nullable: false),
                    transaction_id = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_account_total_shares",
                table: "account",
                column: "total_shares");

            migrationBuilder.CreateIndex(
                name: "IX_account_user_id",
                table: "account",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_association_association_name",
                table: "association",
                column: "association_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_association_Id",
                table: "association",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_loan_taken_id",
                table: "loan_taken",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_loan_taken_loan_type",
                table: "loan_taken",
                column: "loan_type");

            migrationBuilder.CreateIndex(
                name: "IX_loan_taken_modifier_id",
                table: "loan_taken",
                column: "modifier_id");

            migrationBuilder.CreateIndex(
                name: "IX_loan_taken_status",
                table: "loan_taken",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_loan_taken_user_id",
                table: "loan_taken",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_loan_type_id",
                table: "loan_type",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_loan_type_name",
                table: "loan_type",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_loanRepayment_creator_id",
                table: "loanRepayment",
                column: "creator_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_loanRepayment_id",
                table: "loanRepayment",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_loanRepayment_loan_id",
                table: "loanRepayment",
                column: "loan_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_loanRepayment_payment_date",
                table: "loanRepayment",
                column: "payment_date");

            migrationBuilder.CreateIndex(
                name: "IX_loanRepayment_transaction_id",
                table: "loanRepayment",
                column: "transaction_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_role_role_name",
                table: "role",
                column: "role_name");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_user_id",
                table: "transaction",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_association_id",
                table: "user",
                column: "association_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_email",
                table: "user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_phone",
                table: "user",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_role_id",
                table: "user",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account");

            migrationBuilder.DropTable(
                name: "association");

            migrationBuilder.DropTable(
                name: "loan_taken");

            migrationBuilder.DropTable(
                name: "loan_type");

            migrationBuilder.DropTable(
                name: "loanRepayment");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "transaction");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
