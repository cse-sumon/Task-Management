using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class DueDatechangetoDateOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "DueDate",
                table: "Tasks",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "edc267ec-d43c-4e3b-8108-a1a1f819906d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "94ea4700-303d-448e-a539-d661f6d2d226", "AQAAAAIAAYagAAAAEDLgPIVaxx3rWhfKegNsgrMMZjeKEGoaSvCqMxVUn64JyeN4M2LDNePrisqxgsXNcw==", "20932f99-d421-4b8e-a5f0-f71cba741345" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "edc267ec-d43c-4e3b-8108-a1a1f819906d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "76c32a09-af1a-45eb-a78a-fe1efb1deffe", "AQAAAAIAAYagAAAAENJRc5X6QfS/SmZl9QZsifS1nZAQOnkD/7uz90sa6i4VByMX6rjOmWrUixEsRXKjWQ==", "bee8a761-de31-4699-8ff5-b9812b3c75fb" });
        }
    }
}
