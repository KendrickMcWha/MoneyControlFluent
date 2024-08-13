using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyControl.Domain.Migrations
{
    /// <inheritdoc />
    public partial class TransType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_SUBTRANSACTIONS",
            //    table: "SUBTRANSACTIONS");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_PAYEEDETAILS",
            //    table: "PAYEEDETAILS");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_AllTransactionTypes",
            //    table: "AllTransactionTypes");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_AllPayees",
            //    table: "AllPayees");

            //migrationBuilder.RenameTable(
            //    name: "SUBTRANSACTIONS",
            //    newName: "SUBTRANSACTION");

            //migrationBuilder.RenameTable(
            //    name: "PAYEEDETAILS",
            //    newName: "PAYEEDETAIL");

            //migrationBuilder.RenameTable(
            //    name: "AllTransactionTypes",
            //    newName: "TRANSACTIONTYPE");

            //migrationBuilder.RenameTable(
            //    name: "AllPayees",
            //    newName: "PAYEE");

            migrationBuilder.AddColumn<int>(
                name: "TransType",
                table: "TRANSACTIONS",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SUBTRANSACTION",
                table: "SUBTRANSACTION",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PAYEEDETAIL",
                table: "PAYEEDETAIL",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TRANSACTIONTYPE",
                table: "TRANSACTIONTYPE",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PAYEE",
                table: "PAYEE",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TRANSACTIONTYPE",
                table: "TRANSACTIONTYPE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SUBTRANSACTION",
                table: "SUBTRANSACTION");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PAYEEDETAIL",
                table: "PAYEEDETAIL");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PAYEE",
                table: "PAYEE");

            migrationBuilder.DropColumn(
                name: "TransType",
                table: "TRANSACTIONS");

            migrationBuilder.RenameTable(
                name: "TRANSACTIONTYPE",
                newName: "AllTransactionTypes");

            migrationBuilder.RenameTable(
                name: "SUBTRANSACTION",
                newName: "SUBTRANSACTIONS");

            migrationBuilder.RenameTable(
                name: "PAYEEDETAIL",
                newName: "PAYEEDETAILS");

            migrationBuilder.RenameTable(
                name: "PAYEE",
                newName: "AllPayees");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AllTransactionTypes",
                table: "AllTransactionTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SUBTRANSACTIONS",
                table: "SUBTRANSACTIONS",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PAYEEDETAILS",
                table: "PAYEEDETAILS",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AllPayees",
                table: "AllPayees",
                column: "Id");
        }
    }
}
