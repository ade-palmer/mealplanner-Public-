using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MealPlanner365.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealPlans",
                columns: table => new
                {
                    MealPlanId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    WeekStartDay = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealPlans", x => x.MealPlanId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    EnrollmentDateTime = table.Column<DateTimeOffset>(nullable: false),
                    MealPlanId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_MealPlans_MealPlanId",
                        column: x => x.MealPlanId,
                        principalTable: "MealPlans",
                        principalColumn: "MealPlanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FoodTypes",
                columns: table => new
                {
                    FoodTypeId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    MealPlanId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodTypes", x => x.FoodTypeId);
                    table.ForeignKey(
                        name: "FK_FoodTypes_MealPlans_MealPlanId",
                        column: x => x.MealPlanId,
                        principalTable: "MealPlans",
                        principalColumn: "MealPlanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    MealId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    MealPlanId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.MealId);
                    table.ForeignKey(
                        name: "FK_Meals_MealPlans_MealPlanId",
                        column: x => x.MealPlanId,
                        principalTable: "MealPlans",
                        principalColumn: "MealPlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shopping",
                columns: table => new
                {
                    ShoppingId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    MealPlanId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shopping", x => x.ShoppingId);
                    table.ForeignKey(
                        name: "FK_Shopping_MealPlans_MealPlanId",
                        column: x => x.MealPlanId,
                        principalTable: "MealPlans",
                        principalColumn: "MealPlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    FoodTypeId = table.Column<Guid>(nullable: false),
                    MealPlanId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Items_FoodTypes_FoodTypeId",
                        column: x => x.FoodTypeId,
                        principalTable: "FoodTypes",
                        principalColumn: "FoodTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_MealPlans_MealPlanId",
                        column: x => x.MealPlanId,
                        principalTable: "MealPlans",
                        principalColumn: "MealPlanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserMeals",
                columns: table => new
                {
                    UserId = table.Column<string>(maxLength: 450, nullable: false),
                    MealId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMeals", x => new { x.UserId, x.MealId });
                    table.ForeignKey(
                        name: "FK_UserMeals_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "MealId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMeals_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealItems",
                columns: table => new
                {
                    MealId = table.Column<Guid>(nullable: false),
                    ItemId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealItems", x => new { x.MealId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_MealItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealItems_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "MealId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "f8bcb1e0-6746-4ac7-b967-fe0c3e5fff7c", "a840cd08-3f50-43cc-8e6b-73d3de87abee", "Administrator", "ADMINISTRATOR" },
                    { "a23ba371-39dc-40de-a163-4d22c9dcdc93", "30963960-3582-47f6-9b0e-5feca79aa0d1", "Creator", "CREATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "EnrollmentDateTime", "LockoutEnabled", "LockoutEnd", "MealPlanId", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e5976876-269c-486e-9ec0-7d90aae2b59d", 0, "1e4d6419-ddcc-400b-b49b-149d8237ccde", "administrator@mealplanner365.com", true, new DateTimeOffset(new DateTime(2020, 6, 2, 13, 43, 32, 524, DateTimeKind.Unspecified).AddTicks(8950), new TimeSpan(0, 1, 0, 0, 0)), false, null, null, "ADMINISTRATOR@MEALPLANNER365.COM", "ADMIN", "AQAAAAEAACcQAAAAENx1PNZ3qpxXYn/0eflPijByMeu9Ylf9RDWfzZMIMtHmX/br3JgmmcsIx75EsJaW7Q==", null, false, "", false, "Admin" });

            migrationBuilder.InsertData(
                table: "MealPlans",
                columns: new[] { "MealPlanId", "Description", "Name", "WeekStartDay" },
                values: new object[] { new Guid("0c1574aa-a75a-4b57-bbfa-e63cac9c4a96"), "Palmer Dinners", "Palmer Dinners", 1 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "e5976876-269c-486e-9ec0-7d90aae2b59d", "f8bcb1e0-6746-4ac7-b967-fe0c3e5fff7c" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "EnrollmentDateTime", "LockoutEnabled", "LockoutEnd", "MealPlanId", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "daaeb138-c239-4dd4-a83f-c08fbf8ead52", 0, "e4a6300a-92b0-4f49-b029-15dccbfdee52", "ade.palmer@gmail.com", true, new DateTimeOffset(new DateTime(2020, 6, 2, 13, 43, 32, 552, DateTimeKind.Unspecified).AddTicks(9626), new TimeSpan(0, 1, 0, 0, 0)), false, null, new Guid("0c1574aa-a75a-4b57-bbfa-e63cac9c4a96"), "ADE.PALMER@GMAIL.COM", "ADE", "AQAAAAEAACcQAAAAEKkZy+NvKvrb5Otb1LHX78KZ677x09zwy2mBgDmJp/j9JUnERoLPi7rheX/EaWLVIg==", null, false, "", false, "Ade" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "daaeb138-c239-4dd4-a83f-c08fbf8ead52", "a23ba371-39dc-40de-a163-4d22c9dcdc93" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MealPlanId",
                table: "AspNetUsers",
                column: "MealPlanId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FoodTypes_MealPlanId",
                table: "FoodTypes",
                column: "MealPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_FoodTypeId",
                table: "Items",
                column: "FoodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_MealPlanId",
                table: "Items",
                column: "MealPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_MealItems_ItemId",
                table: "MealItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_MealPlanId",
                table: "Meals",
                column: "MealPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Shopping_MealPlanId",
                table: "Shopping",
                column: "MealPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMeals_MealId",
                table: "UserMeals",
                column: "MealId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "MealItems");

            migrationBuilder.DropTable(
                name: "Shopping");

            migrationBuilder.DropTable(
                name: "UserMeals");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "FoodTypes");

            migrationBuilder.DropTable(
                name: "MealPlans");
        }
    }
}
