using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentGateway.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "card",
                columns: table => new
                {
                    Id = table.Column<string>(unicode: false, maxLength: 64, nullable: false),
                    ExpiryDate = table.Column<string>(unicode: false, maxLength: 4, nullable: true),
                    CardNumber = table.Column<string>(unicode: false, maxLength: 64, nullable: true),
                    TransactionLimit = table.Column<decimal>(type: "decimal(7,2)", nullable: true),
                    DayLimit = table.Column<decimal>(type: "decimal(7,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_card", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "payment",
                columns: table => new
                {
                    Id = table.Column<string>(unicode: false, maxLength: 64, nullable: true),
                    CardId = table.Column<string>(unicode: false, maxLength: 64, nullable: true),
                    DateofTransaction = table.Column<DateTime>(nullable: true),
                    Accepted = table.Column<byte>(type: "tinyint(1)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(10,0)", nullable: true),
                    Currency = table.Column<string>(unicode: false, maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "card");

            migrationBuilder.DropTable(
                name: "payment");
        }
    }
}
