using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class ChangePMReportFormRTUIDInForeignKeyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PMChamberMagneticContacts_PMReportFormRTU_PMReportFormID",
                table: "PMChamberMagneticContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_PMDVREquipments_PMReportFormRTU_PMReportFormID",
                table: "PMDVREquipments");

            migrationBuilder.DropForeignKey(
                name: "FK_PMMainRtuCabinets_PMReportFormRTU_PMReportFormID",
                table: "PMMainRtuCabinets");

            migrationBuilder.DropForeignKey(
                name: "FK_PMRTUCabinetCoolings_PMReportFormRTU_PMReportFormID",
                table: "PMRTUCabinetCoolings");

            migrationBuilder.RenameColumn(
                name: "PMReportFormID",
                table: "PMRTUCabinetCoolings",
                newName: "PMReportFormRTUID");

            migrationBuilder.RenameIndex(
                name: "IX_PMRTUCabinetCoolings_PMReportFormID",
                table: "PMRTUCabinetCoolings",
                newName: "IX_PMRTUCabinetCoolings_PMReportFormRTUID");

            migrationBuilder.RenameColumn(
                name: "PMReportFormID",
                table: "PMMainRtuCabinets",
                newName: "PMReportFormRTUID");

            migrationBuilder.RenameIndex(
                name: "IX_PMMainRtuCabinets_PMReportFormID",
                table: "PMMainRtuCabinets",
                newName: "IX_PMMainRtuCabinets_PMReportFormRTUID");

            migrationBuilder.RenameColumn(
                name: "PMReportFormID",
                table: "PMDVREquipments",
                newName: "PMReportFormRTUID");

            migrationBuilder.RenameIndex(
                name: "IX_PMDVREquipments_PMReportFormID",
                table: "PMDVREquipments",
                newName: "IX_PMDVREquipments_PMReportFormRTUID");

            migrationBuilder.RenameColumn(
                name: "PMReportFormID",
                table: "PMChamberMagneticContacts",
                newName: "PMReportFormRTUID");

            migrationBuilder.RenameIndex(
                name: "IX_PMChamberMagneticContacts_PMReportFormID",
                table: "PMChamberMagneticContacts",
                newName: "IX_PMChamberMagneticContacts_PMReportFormRTUID");

            migrationBuilder.AddForeignKey(
                name: "FK_PMChamberMagneticContacts_PMReportFormRTU_PMReportFormRTUID",
                table: "PMChamberMagneticContacts",
                column: "PMReportFormRTUID",
                principalTable: "PMReportFormRTU",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMDVREquipments_PMReportFormRTU_PMReportFormRTUID",
                table: "PMDVREquipments",
                column: "PMReportFormRTUID",
                principalTable: "PMReportFormRTU",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMMainRtuCabinets_PMReportFormRTU_PMReportFormRTUID",
                table: "PMMainRtuCabinets",
                column: "PMReportFormRTUID",
                principalTable: "PMReportFormRTU",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMRTUCabinetCoolings_PMReportFormRTU_PMReportFormRTUID",
                table: "PMRTUCabinetCoolings",
                column: "PMReportFormRTUID",
                principalTable: "PMReportFormRTU",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PMChamberMagneticContacts_PMReportFormRTU_PMReportFormRTUID",
                table: "PMChamberMagneticContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_PMDVREquipments_PMReportFormRTU_PMReportFormRTUID",
                table: "PMDVREquipments");

            migrationBuilder.DropForeignKey(
                name: "FK_PMMainRtuCabinets_PMReportFormRTU_PMReportFormRTUID",
                table: "PMMainRtuCabinets");

            migrationBuilder.DropForeignKey(
                name: "FK_PMRTUCabinetCoolings_PMReportFormRTU_PMReportFormRTUID",
                table: "PMRTUCabinetCoolings");

            migrationBuilder.RenameColumn(
                name: "PMReportFormRTUID",
                table: "PMRTUCabinetCoolings",
                newName: "PMReportFormID");

            migrationBuilder.RenameIndex(
                name: "IX_PMRTUCabinetCoolings_PMReportFormRTUID",
                table: "PMRTUCabinetCoolings",
                newName: "IX_PMRTUCabinetCoolings_PMReportFormID");

            migrationBuilder.RenameColumn(
                name: "PMReportFormRTUID",
                table: "PMMainRtuCabinets",
                newName: "PMReportFormID");

            migrationBuilder.RenameIndex(
                name: "IX_PMMainRtuCabinets_PMReportFormRTUID",
                table: "PMMainRtuCabinets",
                newName: "IX_PMMainRtuCabinets_PMReportFormID");

            migrationBuilder.RenameColumn(
                name: "PMReportFormRTUID",
                table: "PMDVREquipments",
                newName: "PMReportFormID");

            migrationBuilder.RenameIndex(
                name: "IX_PMDVREquipments_PMReportFormRTUID",
                table: "PMDVREquipments",
                newName: "IX_PMDVREquipments_PMReportFormID");

            migrationBuilder.RenameColumn(
                name: "PMReportFormRTUID",
                table: "PMChamberMagneticContacts",
                newName: "PMReportFormID");

            migrationBuilder.RenameIndex(
                name: "IX_PMChamberMagneticContacts_PMReportFormRTUID",
                table: "PMChamberMagneticContacts",
                newName: "IX_PMChamberMagneticContacts_PMReportFormID");

            migrationBuilder.AddForeignKey(
                name: "FK_PMChamberMagneticContacts_PMReportFormRTU_PMReportFormID",
                table: "PMChamberMagneticContacts",
                column: "PMReportFormID",
                principalTable: "PMReportFormRTU",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMDVREquipments_PMReportFormRTU_PMReportFormID",
                table: "PMDVREquipments",
                column: "PMReportFormID",
                principalTable: "PMReportFormRTU",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMMainRtuCabinets_PMReportFormRTU_PMReportFormID",
                table: "PMMainRtuCabinets",
                column: "PMReportFormID",
                principalTable: "PMReportFormRTU",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMRTUCabinetCoolings_PMReportFormRTU_PMReportFormID",
                table: "PMRTUCabinetCoolings",
                column: "PMReportFormID",
                principalTable: "PMReportFormRTU",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
