using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeesManagement.Migrations
{
    /// <inheritdoc />
    public partial class ApprovalNote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovalNote",
                table: "LeaveApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalNote",
                table: "LeaveApplications");
        }
    }
}
