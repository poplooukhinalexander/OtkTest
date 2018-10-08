﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OtkTest.Models;

namespace OtkTest.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OtkTest.Models.Account", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountTypeId");

                    b.Property<int>("BankId");

                    b.Property<int>("CurrencyId");

                    b.Property<decimal>("Money");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<byte[]>("RowVerion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("AccountTypeId");

                    b.HasIndex("BankId");

                    b.HasIndex("CurrencyId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("OtkTest.Models.AccountType", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Description")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("AccountTypes");
                });

            modelBuilder.Entity("OtkTest.Models.AccountTypeCommission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("CommissionPercent");

                    b.Property<int>("RecepientAccountTypeId");

                    b.Property<int>("SenderAccountTypeId");

                    b.HasKey("Id");

                    b.HasIndex("RecepientAccountTypeId");

                    b.HasIndex("SenderAccountTypeId");

                    b.ToTable("AccountTypeCommissions");
                });

            modelBuilder.Entity("OtkTest.Models.Bank", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("LongName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("NeedTransactionConfirm");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("OtkTest.Models.BankCommission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BankCommissionTypeId");

                    b.Property<int>("BankId");

                    b.Property<float>("CommissionPercent");

                    b.Property<DateTime>("SetupAt");

                    b.HasKey("Id");

                    b.HasIndex("BankCommissionTypeId");

                    b.HasIndex("BankId");

                    b.ToTable("BankCommissions");
                });

            modelBuilder.Entity("OtkTest.Models.BankCommissionType", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Description")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("BankCommissionTypes");

                    b.HasData(
                        new { Id = 1, Description = "", Name = "Внутренний перевод" },
                        new { Id = 2, Description = "", Name = "Перевод в другой банк" }
                    );
                });

            modelBuilder.Entity("OtkTest.Models.Currency", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("LongName")
                        .HasMaxLength(50);

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(5);

                    b.HasKey("Id");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("OtkTest.Models.Transaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AccountTypeCommissionId");

                    b.Property<decimal>("Amount");

                    b.Property<long>("BankCommisionId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<long>("RecepientAccountId");

                    b.Property<long>("SenderAccountId");

                    b.HasKey("Id");

                    b.HasIndex("AccountTypeCommissionId");

                    b.HasIndex("BankCommisionId");

                    b.HasIndex("RecepientAccountId");

                    b.HasIndex("SenderAccountId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("OtkTest.Models.Account", b =>
                {
                    b.HasOne("OtkTest.Models.AccountType", "AccountType")
                        .WithMany("Accounts")
                        .HasForeignKey("AccountTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OtkTest.Models.Bank", "Bank")
                        .WithMany("Accounts")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OtkTest.Models.Currency", "Currency")
                        .WithMany("Accounts")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("OtkTest.Models.AccountTypeCommission", b =>
                {
                    b.HasOne("OtkTest.Models.AccountType", "RecepientAccountType")
                        .WithMany()
                        .HasForeignKey("RecepientAccountTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OtkTest.Models.AccountType", "SenderAccountType")
                        .WithMany()
                        .HasForeignKey("SenderAccountTypeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("OtkTest.Models.BankCommission", b =>
                {
                    b.HasOne("OtkTest.Models.BankCommissionType", "BankCommissionType")
                        .WithMany()
                        .HasForeignKey("BankCommissionTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OtkTest.Models.Bank", "Bank")
                        .WithMany("Commissions")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("OtkTest.Models.Transaction", b =>
                {
                    b.HasOne("OtkTest.Models.AccountTypeCommission", "AccountTypeCommission")
                        .WithMany()
                        .HasForeignKey("AccountTypeCommissionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OtkTest.Models.BankCommission", "BankCommission")
                        .WithMany()
                        .HasForeignKey("BankCommisionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OtkTest.Models.Account", "RecepientAccount")
                        .WithMany()
                        .HasForeignKey("RecepientAccountId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OtkTest.Models.Account", "SenderAccount")
                        .WithMany()
                        .HasForeignKey("SenderAccountId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
