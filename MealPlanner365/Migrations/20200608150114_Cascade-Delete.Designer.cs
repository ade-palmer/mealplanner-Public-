﻿// <auto-generated />
using System;
using MealPlanner365.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MealPlanner365.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200608150114_Cascade-Delete")]
    partial class CascadeDelete
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MealPlanner365.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("EnrollmentDateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("MealPlanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

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
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("MealPlanId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = "e5976876-269c-486e-9ec0-7d90aae2b59d",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "8548a67f-30fb-4bfa-91e8-80743335ecaf",
                            Email = "administrator@mealplanner365.com",
                            EmailConfirmed = true,
                            EnrollmentDateTime = new DateTimeOffset(new DateTime(2020, 6, 8, 16, 1, 13, 819, DateTimeKind.Unspecified).AddTicks(6891), new TimeSpan(0, 1, 0, 0, 0)),
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMINISTRATOR@MEALPLANNER365.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAEAACcQAAAAEPK818PTXWABb0BcOdmjTwDIh+uz3R+s188hKlH6dgkhhQixt6kdM/B/T6ezOoyPLg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "",
                            TwoFactorEnabled = false,
                            UserName = "Admin"
                        },
                        new
                        {
                            Id = "daaeb138-c239-4dd4-a83f-c08fbf8ead52",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "ddb2c63f-29fe-4af4-914c-aea99904c530",
                            Email = "ade.palmer@gmail.com",
                            EmailConfirmed = true,
                            EnrollmentDateTime = new DateTimeOffset(new DateTime(2020, 6, 8, 16, 1, 13, 856, DateTimeKind.Unspecified).AddTicks(8648), new TimeSpan(0, 1, 0, 0, 0)),
                            LockoutEnabled = false,
                            MealPlanId = new Guid("0c1574aa-a75a-4b57-bbfa-e63cac9c4a96"),
                            NormalizedEmail = "ADE.PALMER@GMAIL.COM",
                            NormalizedUserName = "ADE",
                            PasswordHash = "AQAAAAEAACcQAAAAEJRRqsmAvaw035bJX223UcI/RtOYxWp1SLSPepwJI5m1G8YPYTQv3Bbqrt3lS+G9YQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "",
                            TwoFactorEnabled = false,
                            UserName = "Ade"
                        });
                });

            modelBuilder.Entity("MealPlanner365.Entities.FoodType", b =>
                {
                    b.Property<Guid>("FoodTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MealPlanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("FoodTypeId");

                    b.HasIndex("MealPlanId");

                    b.ToTable("FoodTypes");
                });

            modelBuilder.Entity("MealPlanner365.Entities.Item", b =>
                {
                    b.Property<Guid>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FoodTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MealPlanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("ItemId");

                    b.HasIndex("FoodTypeId");

                    b.HasIndex("MealPlanId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("MealPlanner365.Entities.Meal", b =>
                {
                    b.Property<Guid>("MealId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("MealPlanId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MealId");

                    b.HasIndex("MealPlanId");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("MealPlanner365.Entities.MealItem", b =>
                {
                    b.Property<Guid>("MealId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MealId", "ItemId");

                    b.HasIndex("ItemId");

                    b.ToTable("MealItems");
                });

            modelBuilder.Entity("MealPlanner365.Entities.MealPlan", b =>
                {
                    b.Property<Guid>("MealPlanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("WeekStartDay")
                        .HasColumnType("int");

                    b.HasKey("MealPlanId");

                    b.ToTable("MealPlans");

                    b.HasData(
                        new
                        {
                            MealPlanId = new Guid("0c1574aa-a75a-4b57-bbfa-e63cac9c4a96"),
                            Description = "Palmer Dinners",
                            Name = "Palmer Dinners",
                            WeekStartDay = 1
                        });
                });

            modelBuilder.Entity("MealPlanner365.Entities.Shopping", b =>
                {
                    b.Property<Guid>("ShoppingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("MealPlanId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ShoppingId");

                    b.HasIndex("MealPlanId");

                    b.ToTable("Shopping");
                });

            modelBuilder.Entity("MealPlanner365.Entities.UserMeal", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450);

                    b.Property<Guid>("MealId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "MealId");

                    b.HasIndex("MealId");

                    b.ToTable("UserMeals");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "f8bcb1e0-6746-4ac7-b967-fe0c3e5fff7c",
                            ConcurrencyStamp = "90a3f753-62bf-4e57-a35f-001a10ab8098",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = "a23ba371-39dc-40de-a163-4d22c9dcdc93",
                            ConcurrencyStamp = "2c19a0c9-5fd4-4de7-91f8-3fe6168b110d",
                            Name = "Creator",
                            NormalizedName = "CREATOR"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = "e5976876-269c-486e-9ec0-7d90aae2b59d",
                            RoleId = "f8bcb1e0-6746-4ac7-b967-fe0c3e5fff7c"
                        },
                        new
                        {
                            UserId = "daaeb138-c239-4dd4-a83f-c08fbf8ead52",
                            RoleId = "a23ba371-39dc-40de-a163-4d22c9dcdc93"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MealPlanner365.Entities.ApplicationUser", b =>
                {
                    b.HasOne("MealPlanner365.Entities.MealPlan", "MealPlan")
                        .WithMany("User")
                        .HasForeignKey("MealPlanId");
                });

            modelBuilder.Entity("MealPlanner365.Entities.FoodType", b =>
                {
                    b.HasOne("MealPlanner365.Entities.MealPlan", "MealPlan")
                        .WithMany("FoodTypes")
                        .HasForeignKey("MealPlanId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("MealPlanner365.Entities.Item", b =>
                {
                    b.HasOne("MealPlanner365.Entities.FoodType", "FoodType")
                        .WithMany("Items")
                        .HasForeignKey("FoodTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MealPlanner365.Entities.MealPlan", "MealPlan")
                        .WithMany("Items")
                        .HasForeignKey("MealPlanId");
                });

            modelBuilder.Entity("MealPlanner365.Entities.Meal", b =>
                {
                    b.HasOne("MealPlanner365.Entities.MealPlan", "MealPlan")
                        .WithMany("Meals")
                        .HasForeignKey("MealPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MealPlanner365.Entities.MealItem", b =>
                {
                    b.HasOne("MealPlanner365.Entities.Item", "Item")
                        .WithMany("MealItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MealPlanner365.Entities.Meal", "Meal")
                        .WithMany("MealItems")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MealPlanner365.Entities.Shopping", b =>
                {
                    b.HasOne("MealPlanner365.Entities.MealPlan", "MealPlan")
                        .WithMany("Shopping")
                        .HasForeignKey("MealPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MealPlanner365.Entities.UserMeal", b =>
                {
                    b.HasOne("MealPlanner365.Entities.Meal", "Meal")
                        .WithMany("UserMeals")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MealPlanner365.Entities.ApplicationUser", "User")
                        .WithMany("UserMeals")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
                    b.HasOne("MealPlanner365.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MealPlanner365.Entities.ApplicationUser", null)
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

                    b.HasOne("MealPlanner365.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MealPlanner365.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}