using System;
using System.Linq;

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OtkTest.Migrations
{
    using Models;

    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankCommissionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankCommissionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ShortName = table.Column<string>(maxLength: 10, nullable: false),
                    LongName = table.Column<string>(maxLength: 50, nullable: false),
                    NeedTransactionConfirm = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ShortName = table.Column<string>(maxLength: 5, nullable: false),
                    LongName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountTypeCommissions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SenderAccountTypeId = table.Column<int>(nullable: false),
                    RecepientAccountTypeId = table.Column<int>(nullable: false),
                    CommissionPercent = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTypeCommissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountTypeCommissions_AccountTypes_RecepientAccountTypeId",
                        column: x => x.RecepientAccountTypeId,
                        principalTable: "AccountTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountTypeCommissions_AccountTypes_SenderAccountTypeId",
                        column: x => x.SenderAccountTypeId,
                        principalTable: "AccountTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankCommissions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SetupAt = table.Column<DateTime>(nullable: false),
                    CommissionPercent = table.Column<float>(nullable: false),
                    BankId = table.Column<int>(nullable: false),
                    BankCommissionTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankCommissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankCommissions_BankCommissionTypes_BankCommissionTypeId",
                        column: x => x.BankCommissionTypeId,
                        principalTable: "BankCommissionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankCommissions_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<string>(maxLength: 20, nullable: false),
                    Money = table.Column<decimal>(nullable: false),
                    BankId = table.Column<int>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    AccountTypeId = table.Column<int>(nullable: false),
                    RowVerion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_AccountTypes_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalTable: "AccountTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    SenderAccountId = table.Column<long>(nullable: false),
                    RecepientAccountId = table.Column<long>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    BankCommisionId = table.Column<long>(nullable: false),
                    AccountTypeCommissionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_AccountTypeCommissions_AccountTypeCommissionId",
                        column: x => x.AccountTypeCommissionId,
                        principalTable: "AccountTypeCommissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_BankCommissions_BankCommisionId",
                        column: x => x.BankCommisionId,
                        principalTable: "BankCommissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_RecepientAccountId",
                        column: x => x.RecepientAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_SenderAccountId",
                        column: x => x.SenderAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });          

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountTypeId",
                table: "Accounts",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_BankId",
                table: "Accounts",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CurrencyId",
                table: "Accounts",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTypeCommissions_RecepientAccountTypeId",
                table: "AccountTypeCommissions",
                column: "RecepientAccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTypeCommissions_SenderAccountTypeId",
                table: "AccountTypeCommissions",
                column: "SenderAccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BankCommissions_BankCommissionTypeId",
                table: "BankCommissions",
                column: "BankCommissionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BankCommissions_BankId",
                table: "BankCommissions",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountTypeCommissionId",
                table: "Transactions",
                column: "AccountTypeCommissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BankCommisionId",
                table: "Transactions",
                column: "BankCommisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_RecepientAccountId",
                table: "Transactions",
                column: "RecepientAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SenderAccountId",
                table: "Transactions",
                column: "SenderAccountId");

            InitData(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "AccountTypeCommissions");

            migrationBuilder.DropTable(
                name: "BankCommissions");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "BankCommissionTypes");

            migrationBuilder.DropTable(
                name: "AccountTypes");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Currencies");
        }

        private void InitData(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Banks", new string[] { "Id", "ShortName", "LongName", "NeedTransactionConfirm" }, new object[,] 
            {
                { Bank.Sber, "Сбербанк", "Сбербанк", false },
                { Bank.Vtb, "ВТБ", "ВТБ", false },
                { Bank.Alfa, "Альфа-банк", "Альфа-банк", true }
            });

            migrationBuilder.InsertData("BankCommissionTypes", new string[] { "Id", "Name" }, new object[,] 
            {
                { BankCommissionType.Internal, "Внутренние переводы" },
                { BankCommissionType.External, "Внешние переводы" }
            });

            migrationBuilder.InsertData("BankCommissions", new string[] { "SetupAt", "CommissionPercent", "BankId", "BankCommissionTypeId" },
                new object[,] 
            {
                { DateTime.Now, 0.0F, Bank.Sber, BankCommissionType.Internal },
                { DateTime.Now, 1.0F, Bank.Sber, BankCommissionType.External },
                { DateTime.Now, 0.0F, Bank.Vtb, BankCommissionType.Internal },
                { DateTime.Now, 2.0F, Bank.Vtb, BankCommissionType.External },
                { DateTime.Now, 1.0F, Bank.Alfa, BankCommissionType.Internal },
                { DateTime.Now, 2.5F, Bank.Alfa, BankCommissionType.External },
            });

            migrationBuilder.InsertData("AccountTypes", new string[] { "Id", "Name" }, new object[,] 
            {
                { AccountType.IndividualAccountType, "Физ.лицо" },
                { AccountType.CompanyAccountType, "Юр.лицо" },
                { AccountType.NonResidentAccountType, "Нерезидент" }
            });

            migrationBuilder.InsertData("AccountTypeCommissions", new string[] { "SenderAccountTypeId", "RecepientAccountTypeId", "CommissionPercent" }, 
                new object[,] 
            {
                { AccountType.IndividualAccountType, AccountType.IndividualAccountType, 0.0F },
                { AccountType.IndividualAccountType, AccountType.CompanyAccountType, 0.0F },
                { AccountType.IndividualAccountType, AccountType.NonResidentAccountType, 0.0F },
                { AccountType.CompanyAccountType, AccountType.IndividualAccountType, 2.0F },
                { AccountType.CompanyAccountType, AccountType.CompanyAccountType, 3.0F },
                { AccountType.CompanyAccountType, AccountType.NonResidentAccountType, 4.0F },
                { AccountType.NonResidentAccountType, AccountType.IndividualAccountType, 4.0F },
                { AccountType.NonResidentAccountType, AccountType.CompanyAccountType, 6.0F },
                { AccountType.NonResidentAccountType, AccountType.NonResidentAccountType, 6.0F },
            });

            migrationBuilder.InsertData("Currencies", new string[] { "Id", "ShortName" }, new object[,] { { 643, "RUB" } });

            var randomizer = new Random();
            var accounts = new object[300, 5];
            for(int i = 0; i < 300; i ++)
            {
                accounts[i, 0] = string.Join("", Enumerable.Repeat(0, 16).Select(x => randomizer.Next(0, 9)));
                accounts[i, 1] = randomizer.Next(0, 10000);
                accounts[i, 2] = randomizer.Next(1, 4);
                accounts[i, 3] = randomizer.Next(1, 4);
                accounts[i, 4] = 643;
            }
            migrationBuilder.InsertData("Accounts", new string[] { "Number", "Money", "BankId", "AccountTypeId", "CurrencyId" }, accounts);
        }
    }
}
