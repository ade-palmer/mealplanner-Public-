using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MealPlanner365.Migrations
{
    public partial class CascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "MealPlanId",
                table: "FoodTypes",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
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
                columns: new[] { "ConcurrencyStamp", "EnrollmentDateTime", "PasswordHash" },
                values: new object[] { "ddb2c63f-29fe-4af4-914c-aea99904c530", new DateTimeOffset(new DateTime(2020, 6, 8, 16, 1, 13, 856, DateTimeKind.Unspecified).AddTicks(8648), new TimeSpan(0, 1, 0, 0, 0)), "AQAAAAEAACcQAAAAEJRRqsmAvaw035bJX223UcI/RtOYxWp1SLSPepwJI5m1G8YPYTQv3Bbqrt3lS+G9YQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e5976876-269c-486e-9ec0-7d90aae2b59d",
                columns: new[] { "ConcurrencyStamp", "EnrollmentDateTime", "PasswordHash" },
                values: new object[] { "8548a67f-30fb-4bfa-91e8-80743335ecaf", new DateTimeOffset(new DateTime(2020, 6, 8, 16, 1, 13, 819, DateTimeKind.Unspecified).AddTicks(6891), new TimeSpan(0, 1, 0, 0, 0)), "AQAAAAEAACcQAAAAEPK818PTXWABb0BcOdmjTwDIh+uz3R+s188hKlH6dgkhhQixt6kdM/B/T6ezOoyPLg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "MealPlanId",
                table: "FoodTypes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a23ba371-39dc-40de-a163-4d22c9dcdc93",
                column: "ConcurrencyStamp",
                value: "30963960-3582-47f6-9b0e-5feca79aa0d1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8bcb1e0-6746-4ac7-b967-fe0c3e5fff7c",
                column: "ConcurrencyStamp",
                value: "a840cd08-3f50-43cc-8e6b-73d3de87abee");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "daaeb138-c239-4dd4-a83f-c08fbf8ead52",
                columns: new[] { "ConcurrencyStamp", "EnrollmentDateTime", "PasswordHash" },
                values: new object[] { "e4a6300a-92b0-4f49-b029-15dccbfdee52", new DateTimeOffset(new DateTime(2020, 6, 2, 13, 43, 32, 552, DateTimeKind.Unspecified).AddTicks(9626), new TimeSpan(0, 1, 0, 0, 0)), "AQAAAAEAACcQAAAAEKkZy+NvKvrb5Otb1LHX78KZ677x09zwy2mBgDmJp/j9JUnERoLPi7rheX/EaWLVIg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e5976876-269c-486e-9ec0-7d90aae2b59d",
                columns: new[] { "ConcurrencyStamp", "EnrollmentDateTime", "PasswordHash" },
                values: new object[] { "1e4d6419-ddcc-400b-b49b-149d8237ccde", new DateTimeOffset(new DateTime(2020, 6, 2, 13, 43, 32, 524, DateTimeKind.Unspecified).AddTicks(8950), new TimeSpan(0, 1, 0, 0, 0)), "AQAAAAEAACcQAAAAENx1PNZ3qpxXYn/0eflPijByMeu9Ylf9RDWfzZMIMtHmX/br3JgmmcsIx75EsJaW7Q==" });
        }
    }
}
