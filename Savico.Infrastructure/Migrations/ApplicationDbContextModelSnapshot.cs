﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Savico.Infrastructure;

#nullable disable

namespace Savico.Data.Migrations
{
    [DbContext(typeof(SavicoDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Savico.Core.Models.Budget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Budget identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2")
                        .HasComment("Budget's end date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasComment("Indicates if the budget is soft-deleted");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2")
                        .HasComment("Budget's start date");

                    b.Property<decimal>("TotalAmount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Budget's total amount");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Budgets");
                });

            modelBuilder.Entity("Savico.Core.Models.Expense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Expense identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Expense's amount");

                    b.Property<int>("BudgetId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2")
                        .HasComment("Date of expense");

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)")
                        .HasComment("Expense's description");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasComment("Indicates if the expense is soft-deleted");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BudgetId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("Savico.Core.Models.Goal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Goal identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("CurrentAmount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasComment("User's current amount towards goal");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasComment("Goal description");

                    b.Property<bool>("IsAchieved")
                        .HasColumnType("bit")
                        .HasComment("Indicates if the goal is achieved or not");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasComment("Indicates if the goal is soft-deleted");

                    b.Property<DateTime?>("LastContributionDate")
                        .HasColumnType("datetime2")
                        .HasComment("Last date that a contribution was made on");

                    b.Property<decimal>("MonthlyContribution")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Monthly contribution towards the set goal");

                    b.Property<decimal>("TargetAmount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Goal's target amount");

                    b.Property<DateTime>("TargetDate")
                        .HasColumnType("datetime2")
                        .HasComment("Target date for reaching the goal");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("Savico.Core.Models.Income", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Income identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Income's amount");

                    b.Property<int?>("BudgetId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2")
                        .HasComment("Date of receiving the income");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasComment("Indicates if the income is soft-deleted");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComment("Income's source");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BudgetId");

                    b.HasIndex("UserId");

                    b.ToTable("Incomes");
                });

            modelBuilder.Entity("Savico.Core.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int>("BudgetId")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)")
                        .HasComment("User's currency");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasComment("User's first name");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasComment("Indicates if the user is soft-deleted");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasComment("User's last name");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "ad5a5ccb-2be1-4866-b151-0f09a58416f6",
                            AccessFailedCount = 0,
                            BudgetId = 1,
                            ConcurrencyStamp = "1ae11e18-b982-411d-b8e1-a247a4428011",
                            Currency = "EUR",
                            Email = "testuser123@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Test",
                            IsDeleted = false,
                            LastName = "User",
                            LockoutEnabled = false,
                            NormalizedEmail = "TESTUSER123@GMAIL.COM",
                            NormalizedUserName = "TESTUSER",
                            PasswordHash = "AQAAAAIAAYagAAAAENii02Fu+d8Qg4EYpPY8OoJjonETJyZw+mpgbnWo2u8XpDHZwNUgpeffBvPXm3o9sw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "42fc174b-6f98-427c-a38f-25e3fcdbc2fd",
                            TwoFactorEnabled = false,
                            UserName = "TestUser"
                        },
                        new
                        {
                            Id = "7c55600b-d374-4bfb-8352-a65bf7d44cc1",
                            AccessFailedCount = 0,
                            BudgetId = 2,
                            ConcurrencyStamp = "220bbfb2-d20e-4f9d-90b9-ab26db2dcfee",
                            Currency = "USD",
                            Email = "admin@admin.com",
                            EmailConfirmed = true,
                            FirstName = "TEST",
                            IsDeleted = false,
                            LastName = "ADMIN",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@ADMIN.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAIAAYagAAAAENuBmAloFASTCIXCNeyfyFWRxoZIh3OqHSyWiXQwjYOD19mN2xsZUCkWIN6EXQyDRQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "c243c0c8-8f66-4764-9510-a7fe403c6510",
                            TwoFactorEnabled = false,
                            UserName = "Admin"
                        });
                });

            modelBuilder.Entity("Savico.Infrastructure.Data.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ExpenseId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("ExpenseId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Utilities"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Rent"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Groceries"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Entertainment"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Mortgage"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Property taxes"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Insurance"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Transportation"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Health"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Subscription"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Bank loan"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Donation"
                        },
                        new
                        {
                            Id = 13,
                            Name = "Clothing"
                        });
                });

            modelBuilder.Entity("Savico.Infrastructure.Data.Models.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Report identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2")
                        .HasComment("End date of the report period");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasComment("Indicates if the report is soft-deleted");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2")
                        .HasComment("Start date of the report period");

                    b.Property<decimal>("TotalExpense")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Total expense during the period");

                    b.Property<decimal>("TotalIncome")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasComment("Total income during the period");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasComment("User who created the report");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Savico.Infrastructure.Data.Models.Tip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.ToTable("Tips");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "Buy generic bread, save for your dream yacht."
                        },
                        new
                        {
                            Id = 2,
                            Content = "Don't let your spending get out of control—unless it's a coupon you found in your pocket!"
                        },
                        new
                        {
                            Id = 3,
                            Content = "Budgeting is like dieting. You can’t keep eating chips if you want to fit in your financial goals."
                        },
                        new
                        {
                            Id = 4,
                            Content = "Save your change. Someday, it might be worth a lot... or at least enough for a nice coffee."
                        },
                        new
                        {
                            Id = 5,
                            Content = "It’s not ‘expensive coffee,’ it’s ‘investment in happiness’—unless your budget says otherwise."
                        },
                        new
                        {
                            Id = 6,
                            Content = "Don’t stress about your budget—just make sure your credit card doesn’t get any ideas."
                        },
                        new
                        {
                            Id = 7,
                            Content = "Saving money is like washing your car—you feel like you're not getting anywhere, but it’s worth it in the end."
                        },
                        new
                        {
                            Id = 8,
                            Content = "Don’t spend your money too fast—unless it's on ice cream, because ice cream is always worth it."
                        },
                        new
                        {
                            Id = 9,
                            Content = "Financial freedom is just a few small sacrifices away... starting with not buying that extra pair of shoes."
                        },
                        new
                        {
                            Id = 10,
                            Content = "Be patient and persistent in achieving your financial goals."
                        },
                        new
                        {
                            Id = 11,
                            Content = "Always compare prices before making significant purchases."
                        },
                        new
                        {
                            Id = 12,
                            Content = "Save a portion of any raise or bonus you receive."
                        },
                        new
                        {
                            Id = 13,
                            Content = "Start budgeting and keep track of all income and expenses."
                        },
                        new
                        {
                            Id = 14,
                            Content = "Review your budget regularly to stay on track."
                        },
                        new
                        {
                            Id = 15,
                            Content = "Always plan for the future, not just for the present."
                        },
                        new
                        {
                            Id = 16,
                            Content = "Try to build an emergency fund for unexpected expenses."
                        },
                        new
                        {
                            Id = 17,
                            Content = "Set realistic financial goals and work towards them consistently."
                        },
                        new
                        {
                            Id = 18,
                            Content = "Track your spending to know where your money is going."
                        },
                        new
                        {
                            Id = 19,
                            Content = "Put your savings in a jar... and then hide it from yourself so you can’t spend it!"
                        },
                        new
                        {
                            Id = 20,
                            Content = "Save money by spending it on things that’ll last forever, like a good pair of socks (because socks don’t expire)."
                        },
                        new
                        {
                            Id = 21,
                            Content = "If you save enough money, you can one day buy that thing you don’t need. Think of the possibilities!"
                        },
                        new
                        {
                            Id = 22,
                            Content = "You can’t buy happiness, but you can buy a savings account, and that’s pretty much the same thing."
                        },
                        new
                        {
                            Id = 23,
                            Content = "Turn your coffee habit into a savings plan: swap your latte for instant coffee, then use the savings to buy something ridiculous!"
                        },
                        new
                        {
                            Id = 24,
                            Content = "Save on heating bills by wearing all your clothes at once—fashion is subjective, right?"
                        },
                        new
                        {
                            Id = 25,
                            Content = "Instead of spending money on fancy things, spend time with people who think free stuff is cool."
                        },
                        new
                        {
                            Id = 26,
                            Content = "Save on heating bills by wearing all your clothes at once—fashion is subjective, right?"
                        },
                        new
                        {
                            Id = 27,
                            Content = "Cut costs by cutting back on things you don’t really need, like your subscription to the ‘I like pizza’ fan club."
                        },
                        new
                        {
                            Id = 28,
                            Content = "Put your savings in a piggy bank and then give it a name. If you bond with it, you’ll resist the urge to break it open."
                        },
                        new
                        {
                            Id = 29,
                            Content = "The best way to save money? Stop buying things that seem like a good idea at 2 a.m. after too much coffee."
                        },
                        new
                        {
                            Id = 30,
                            Content = "Saving money is like a treasure hunt. The more you save, the more you realize it was worth the effort—and the loot is much cooler than that impulse buy."
                        },
                        new
                        {
                            Id = 31,
                            Content = "To save more, stop buying things you don’t need and start selling things you forgot you bought."
                        },
                        new
                        {
                            Id = 32,
                            Content = "Remember: Money can’t buy happiness, but it can buy a ton of snacks for your movie marathon. And that’s basically the same thing."
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Savico.Core.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Savico.Core.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Savico.Core.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Savico.Core.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Savico.Core.Models.Budget", b =>
                {
                    b.HasOne("Savico.Core.Models.User", "User")
                        .WithOne("Budget")
                        .HasForeignKey("Savico.Core.Models.Budget", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Savico.Core.Models.Expense", b =>
                {
                    b.HasOne("Savico.Core.Models.Budget", "Budget")
                        .WithMany("Expenses")
                        .HasForeignKey("BudgetId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Savico.Infrastructure.Data.Models.Category", "Category")
                        .WithMany("Expenses")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Savico.Core.Models.User", "User")
                        .WithMany("Expenses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Budget");

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Savico.Core.Models.Goal", b =>
                {
                    b.HasOne("Savico.Core.Models.User", "User")
                        .WithMany("Goals")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Savico.Core.Models.Income", b =>
                {
                    b.HasOne("Savico.Core.Models.Budget", null)
                        .WithMany("Incomes")
                        .HasForeignKey("BudgetId");

                    b.HasOne("Savico.Core.Models.User", "User")
                        .WithMany("Incomes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Savico.Infrastructure.Data.Models.Category", b =>
                {
                    b.HasOne("Savico.Core.Models.Expense", null)
                        .WithMany("Categories")
                        .HasForeignKey("ExpenseId");
                });

            modelBuilder.Entity("Savico.Infrastructure.Data.Models.Report", b =>
                {
                    b.HasOne("Savico.Core.Models.User", "User")
                        .WithMany("Reports")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Savico.Core.Models.Budget", b =>
                {
                    b.Navigation("Expenses");

                    b.Navigation("Incomes");
                });

            modelBuilder.Entity("Savico.Core.Models.Expense", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("Savico.Core.Models.User", b =>
                {
                    b.Navigation("Budget")
                        .IsRequired();

                    b.Navigation("Expenses");

                    b.Navigation("Goals");

                    b.Navigation("Incomes");

                    b.Navigation("Reports");
                });

            modelBuilder.Entity("Savico.Infrastructure.Data.Models.Category", b =>
                {
                    b.Navigation("Expenses");
                });
#pragma warning restore 612, 618
        }
    }
}
