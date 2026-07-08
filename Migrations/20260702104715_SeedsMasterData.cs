using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AspnetCoreMvcFull.Migrations
{
    /// <inheritdoc />
    public partial class SeedsMasterData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "BranchId", "BranchName" },
                values: new object[,]
                {
                    { 1, "Head Office" },
                    { 2, "Pune Branch" },
                    { 3, "Nashik Branch" }
                });

            migrationBuilder.InsertData(
                table: "Designations",
                columns: new[] { "DesignationId", "DesignationName" },
                values: new object[,]
                {
                    { 1, "Assistant Manager" },
                    { 2, "Deputy Manager" },
                    { 3, "Chief Manager" },
                    { 4, "AGM" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { 1, "Maker" },
                    { 2, "Checker" },
                    { 3, "Branch Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "BranchId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "BranchId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "BranchId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Designations",
                keyColumn: "DesignationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Designations",
                keyColumn: "DesignationId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Designations",
                keyColumn: "DesignationId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Designations",
                keyColumn: "DesignationId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3);
        }
    }
}
