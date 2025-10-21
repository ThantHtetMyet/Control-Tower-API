using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePMServerASAFirewallTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommandInput",
                table: "PMServerASAFirewalls",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "PMServerASAFirewalls",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SerialNumber",
                table: "PMServerASAFirewalls",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommandInput",
                table: "PMServerASAFirewalls");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "PMServerASAFirewalls");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "PMServerASAFirewalls");
        }
    }
}
