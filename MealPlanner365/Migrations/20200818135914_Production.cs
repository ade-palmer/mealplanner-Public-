using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MealPlanner365.Migrations
{
    public partial class Production : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_FoodTypes_FoodTypeId",
                table: "Items");

            migrationBuilder.AlterColumn<Guid>(
                name: "FoodTypeId",
                table: "Items",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a23ba371-39dc-40de-a163-4d22c9dcdc93",
                column: "ConcurrencyStamp",
                value: "13d8410e-551a-4eb1-9be6-5c3055674587");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8bcb1e0-6746-4ac7-b967-fe0c3e5fff7c",
                column: "ConcurrencyStamp",
                value: "70bb143e-f5cb-484d-b28c-d81b5337ec01");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "daaeb138-c239-4dd4-a83f-c08fbf8ead52",
                columns: new[] { "ConcurrencyStamp", "Email", "EnrollmentDateTime", "NormalizedEmail", "PasswordHash" },
                values: new object[] { "94858261-3aea-4ff8-847f-23914d428d58", "ade.palmer@mealplanner365.co.uk", new DateTimeOffset(new DateTime(2020, 8, 18, 14, 59, 13, 888, DateTimeKind.Unspecified).AddTicks(9558), new TimeSpan(0, 1, 0, 0, 0)), "ADE.PALMER@MEALPLANNER365.CO.UK", "AQAAAAEAACcQAAAAEPERcav+tB5Gafe75K6JQU57sxrG4iHOqMrNFGJb2t0gTd4AxleG/KgPpIQmld97bw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e5976876-269c-486e-9ec0-7d90aae2b59d",
                columns: new[] { "ConcurrencyStamp", "EnrollmentDateTime", "PasswordHash" },
                values: new object[] { "e7b772e9-2e21-473d-a15a-6682fb435960", new DateTimeOffset(new DateTime(2020, 8, 18, 14, 59, 13, 870, DateTimeKind.Unspecified).AddTicks(8623), new TimeSpan(0, 1, 0, 0, 0)), "AQAAAAEAACcQAAAAEKx4YBEot6ftczQ/RwoD7vNWeuZak6DsjaYgEQQ+CDDW2HlPmqpRlZlVQeRDGPmxFA==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Items_FoodTypes_FoodTypeId",
                table: "Items",
                column: "FoodTypeId",
                principalTable: "FoodTypes",
                principalColumn: "FoodTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_FoodTypes_FoodTypeId",
                table: "Items");

            migrationBuilder.AlterColumn<Guid>(
                name: "FoodTypeId",
                table: "Items",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a23ba371-39dc-40de-a163-4d22c9dcdc93",
                column: "ConcurrencyStamp",
                value: "2c19a0c9-5fd4-4de7-91f8-3fe6168b110d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8bcb1e0-6746-4ac7-b967-fe0c3e5fff7c",
                column: "ConcurrencyStamp",
                value: "90a3f753-62bf-4e57-a35f-001a10ab8098");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "daaeb138-c239-4dd4-a83f-c08fbf8ead52",
                columns: new[] { "ConcurrencyStamp", "Email", "EnrollmentDateTime", "NormalizedEmail", "PasswordHash" },
                values: new object[] { "ddb2c63f-29fe-4af4-914c-aea99904c530", "ade.palmer@gmail.com", new DateTimeOffset(new DateTime(2020, 6, 8, 16, 1, 13, 856, DateTimeKind.Unspecified).AddTicks(8648), new TimeSpan(0, 1, 0, 0, 0)), "ADE.PALMER@GMAIL.COM", "AQAAAAEAACcQAAAAEJRRqsmAvaw035bJX223UcI/RtOYxWp1SLSPepwJI5m1G8YPYTQv3Bbqrt3lS+G9YQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e5976876-269c-486e-9ec0-7d90aae2b59d",
                columns: new[] { "ConcurrencyStamp", "EnrollmentDateTime", "PasswordHash" },
                values: new object[] { "8548a67f-30fb-4bfa-91e8-80743335ecaf", new DateTimeOffset(new DateTime(2020, 6, 8, 16, 1, 13, 819, DateTimeKind.Unspecified).AddTicks(6891), new TimeSpan(0, 1, 0, 0, 0)), "AQAAAAEAACcQAAAAEPK818PTXWABb0BcOdmjTwDIh+uz3R+s188hKlH6dgkhhQixt6kdM/B/T6ezOoyPLg==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Items_FoodTypes_FoodTypeId",
                table: "Items",
                column: "FoodTypeId",
                principalTable: "FoodTypes",
                principalColumn: "FoodTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
