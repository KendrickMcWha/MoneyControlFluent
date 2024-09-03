using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyControl.Domain.Migrations
{
    /// <inheritdoc />
    public partial class _20240903 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegularPaymentId",
                table: "TRANSACTIONS");

            migrationBuilder.RenameColumn(
                name: "Transactionid",
                table: "SUBTRANSACTION",
                newName: "TransactionId");

            migrationBuilder.AlterColumn<int>(
                name: "TransDate",
                table: "TRANSACTIONS",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "PostDate",
                table: "TRANSACTIONS",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "BudgetDate",
                table: "TRANSACTIONS",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "SUBTRANSACTION",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PayeeId",
                table: "SUBTRANSACTION",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RegularPaymentId",
                table: "SUBTRANSACTION",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "SUBTRANSACTION");

            migrationBuilder.DropColumn(
                name: "PayeeId",
                table: "SUBTRANSACTION");

            migrationBuilder.DropColumn(
                name: "RegularPaymentId",
                table: "SUBTRANSACTION");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "SUBTRANSACTION",
                newName: "Transactionid");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "TransDate",
                table: "TRANSACTIONS",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "PostDate",
                table: "TRANSACTIONS",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "BudgetDate",
                table: "TRANSACTIONS",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "RegularPaymentId",
                table: "TRANSACTIONS",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
