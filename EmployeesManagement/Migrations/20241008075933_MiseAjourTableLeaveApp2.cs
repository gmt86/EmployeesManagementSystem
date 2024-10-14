using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeesManagement.Migrations
{
    /// <inheritdoc />
    public partial class MiseAjourTableLeaveApp2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveApplications_SystemCodeDetails_StatusId",
                table: "LeaveApplications");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveApplications_SystemCodeDetails_StatusId",
                table: "LeaveApplications",
                column: "StatusId",
                principalTable: "SystemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveApplications_SystemCodeDetails_StatusId",
                table: "LeaveApplications");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveApplications_SystemCodeDetails_StatusId",
                table: "LeaveApplications",
                column: "StatusId",
                principalTable: "SystemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
