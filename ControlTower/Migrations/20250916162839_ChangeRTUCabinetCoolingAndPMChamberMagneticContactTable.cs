using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRTUCabinetCoolingAndPMChamberMagneticContactTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FAN1",
                table: "PMRTUCabinetCoolings");

            migrationBuilder.DropColumn(
                name: "FAN2",
                table: "PMRTUCabinetCoolings");

            migrationBuilder.DropColumn(
                name: "Chamber1Contact1",
                table: "PMChamberMagneticContacts");

            migrationBuilder.DropColumn(
                name: "Chamber1Contact2",
                table: "PMChamberMagneticContacts");

            migrationBuilder.DropColumn(
                name: "Chamber1Contact3",
                table: "PMChamberMagneticContacts");

            migrationBuilder.DropColumn(
                name: "Chamber1OGBox",
                table: "PMChamberMagneticContacts");

            migrationBuilder.DropColumn(
                name: "Chamber2Contact1",
                table: "PMChamberMagneticContacts");

            migrationBuilder.DropColumn(
                name: "Chamber2Contact2",
                table: "PMChamberMagneticContacts");

            migrationBuilder.DropColumn(
                name: "Chamber2Contact3",
                table: "PMChamberMagneticContacts");

            migrationBuilder.RenameColumn(
                name: "FAN4",
                table: "PMRTUCabinetCoolings",
                newName: "FunctionalStatus");

            migrationBuilder.RenameColumn(
                name: "FAN3",
                table: "PMRTUCabinetCoolings",
                newName: "FanNumber");

            migrationBuilder.RenameColumn(
                name: "Chamber3OGBox",
                table: "PMChamberMagneticContacts",
                newName: "ChamberOGBox");

            migrationBuilder.RenameColumn(
                name: "Chamber3Contact3",
                table: "PMChamberMagneticContacts",
                newName: "ChamberNumber");

            migrationBuilder.RenameColumn(
                name: "Chamber3Contact2",
                table: "PMChamberMagneticContacts",
                newName: "ChamberContact3");

            migrationBuilder.RenameColumn(
                name: "Chamber3Contact1",
                table: "PMChamberMagneticContacts",
                newName: "ChamberContact2");

            migrationBuilder.RenameColumn(
                name: "Chamber2OGBox",
                table: "PMChamberMagneticContacts",
                newName: "ChamberContact1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FunctionalStatus",
                table: "PMRTUCabinetCoolings",
                newName: "FAN4");

            migrationBuilder.RenameColumn(
                name: "FanNumber",
                table: "PMRTUCabinetCoolings",
                newName: "FAN3");

            migrationBuilder.RenameColumn(
                name: "ChamberOGBox",
                table: "PMChamberMagneticContacts",
                newName: "Chamber3OGBox");

            migrationBuilder.RenameColumn(
                name: "ChamberNumber",
                table: "PMChamberMagneticContacts",
                newName: "Chamber3Contact3");

            migrationBuilder.RenameColumn(
                name: "ChamberContact3",
                table: "PMChamberMagneticContacts",
                newName: "Chamber3Contact2");

            migrationBuilder.RenameColumn(
                name: "ChamberContact2",
                table: "PMChamberMagneticContacts",
                newName: "Chamber3Contact1");

            migrationBuilder.RenameColumn(
                name: "ChamberContact1",
                table: "PMChamberMagneticContacts",
                newName: "Chamber2OGBox");

            migrationBuilder.AddColumn<string>(
                name: "FAN1",
                table: "PMRTUCabinetCoolings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FAN2",
                table: "PMRTUCabinetCoolings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Chamber1Contact1",
                table: "PMChamberMagneticContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Chamber1Contact2",
                table: "PMChamberMagneticContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Chamber1Contact3",
                table: "PMChamberMagneticContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Chamber1OGBox",
                table: "PMChamberMagneticContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Chamber2Contact1",
                table: "PMChamberMagneticContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Chamber2Contact2",
                table: "PMChamberMagneticContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Chamber2Contact3",
                table: "PMChamberMagneticContacts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
