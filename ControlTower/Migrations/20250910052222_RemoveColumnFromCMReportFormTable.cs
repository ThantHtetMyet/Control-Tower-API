using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class RemoveColumnFromCMReportFormTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionTakenRemark",
                table: "CMReportForms");

            migrationBuilder.DropColumn(
                name: "IssueFoundRemark",
                table: "CMReportForms");

            migrationBuilder.DropColumn(
                name: "IssueReportedRemark",
                table: "CMReportForms");

            migrationBuilder.RenameColumn(
                name: "DoneBy",
                table: "CMReportForms",
                newName: "AttendedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttendedBy",
                table: "CMReportForms",
                newName: "DoneBy");

            migrationBuilder.AddColumn<string>(
                name: "ActionTakenRemark",
                table: "CMReportForms",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IssueFoundRemark",
                table: "CMReportForms",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IssueReportedRemark",
                table: "CMReportForms",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }
    }
}
