using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.LeaveManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedMoreAuditFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "LeaveTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "LeaveTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "leaveRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "leaveRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "leaveAllocations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "leaveAllocations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedBy", "DateCreated", "DateModified", "ModifiedBy" },
                values: new object[] { null, new DateTime(2023, 11, 23, 13, 19, 38, 121, DateTimeKind.Local).AddTicks(5210), new DateTime(2023, 11, 23, 13, 19, 38, 121, DateTimeKind.Local).AddTicks(5283), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "leaveAllocations");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "leaveAllocations");

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 11, 3, 1, 7, 58, 142, DateTimeKind.Local).AddTicks(9858), new DateTime(2023, 11, 3, 1, 7, 58, 142, DateTimeKind.Local).AddTicks(9917) });
        }
    }
}
