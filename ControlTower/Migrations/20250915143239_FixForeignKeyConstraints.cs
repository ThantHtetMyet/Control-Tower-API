using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKeyConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StationName",
                table: "CMReportForms");

            migrationBuilder.DropColumn(
                name: "SystemDescription",
                table: "CMReportForms");

            migrationBuilder.AddColumn<string>(
                name: "JobNo",
                table: "ReportForms",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "StationNameWarehouseID",
                table: "ReportForms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SystemNameWarehouseID",
                table: "ReportForms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PMReportFormTypes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMReportFormTypes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMReportFormTypes_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PMReportFormTypes_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "SystemNameWarehouses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemNameWarehouses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PMReportForms",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportFormID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfService = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CleaningOfCabinet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttendedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMReportForms", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMReportForms_PMReportFormTypes_PMReportFormTypeID",
                        column: x => x.PMReportFormTypeID,
                        principalTable: "PMReportFormTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMReportForms_ReportForms_ReportFormID",
                        column: x => x.ReportFormID,
                        principalTable: "ReportForms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMReportForms_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMReportForms_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StationNameWarehouses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SystemNameWarehouseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StationNameWarehouses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StationNameWarehouses_SystemNameWarehouses_SystemNameWarehouseID",
                        column: x => x.SystemNameWarehouseID,
                        principalTable: "SystemNameWarehouses",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PMChamberMagneticContacts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chamber1OGBox = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chamber1Contact1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chamber1Contact2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chamber1Contact3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chamber2OGBox = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chamber2Contact1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chamber2Contact2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chamber2Contact3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chamber3OGBox = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chamber3Contact1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chamber3Contact2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chamber3Contact3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMChamberMagneticContacts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMChamberMagneticContacts_PMReportForms_PMReportFormID",
                        column: x => x.PMReportFormID,
                        principalTable: "PMReportForms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMChamberMagneticContacts_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMChamberMagneticContacts_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMDVREquipments",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DVRComm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DVRRAIDComm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeSyncNTPServer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Recording24x7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMDVREquipments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMDVREquipments_PMReportForms_PMReportFormID",
                        column: x => x.PMReportFormID,
                        principalTable: "PMReportForms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMDVREquipments_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMDVREquipments_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMMainRtuCabinets",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RTUCabinet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EquipmentRack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Monitor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MouseKeyboard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPU6000Card = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InputCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MegapopNTU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NetworkRouter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NetworkSwitch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DigitalVideoRecorder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RTUDoorContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PowerSupplyUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UPSTakingOverTest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UPSBattery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMMainRtuCabinets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMMainRtuCabinets_PMReportForms_PMReportFormID",
                        column: x => x.PMReportFormID,
                        principalTable: "PMReportForms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMMainRtuCabinets_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMMainRtuCabinets_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMRTUCabinetCoolings",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FAN1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FAN2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FAN3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FAN4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMRTUCabinetCoolings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMRTUCabinetCoolings_PMReportForms_PMReportFormID",
                        column: x => x.PMReportFormID,
                        principalTable: "PMReportForms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMRTUCabinetCoolings_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMRTUCabinetCoolings_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportForms_StationNameWarehouseID",
                table: "ReportForms",
                column: "StationNameWarehouseID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportForms_SystemNameWarehouseID",
                table: "ReportForms",
                column: "SystemNameWarehouseID");

            migrationBuilder.CreateIndex(
                name: "IX_PMChamberMagneticContacts_CreatedBy",
                table: "PMChamberMagneticContacts",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMChamberMagneticContacts_PMReportFormID",
                table: "PMChamberMagneticContacts",
                column: "PMReportFormID");

            migrationBuilder.CreateIndex(
                name: "IX_PMChamberMagneticContacts_UpdatedBy",
                table: "PMChamberMagneticContacts",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMDVREquipments_CreatedBy",
                table: "PMDVREquipments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMDVREquipments_PMReportFormID",
                table: "PMDVREquipments",
                column: "PMReportFormID");

            migrationBuilder.CreateIndex(
                name: "IX_PMDVREquipments_UpdatedBy",
                table: "PMDVREquipments",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMMainRtuCabinets_CreatedBy",
                table: "PMMainRtuCabinets",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMMainRtuCabinets_PMReportFormID",
                table: "PMMainRtuCabinets",
                column: "PMReportFormID");

            migrationBuilder.CreateIndex(
                name: "IX_PMMainRtuCabinets_UpdatedBy",
                table: "PMMainRtuCabinets",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMReportForms_CreatedBy",
                table: "PMReportForms",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMReportForms_PMReportFormTypeID",
                table: "PMReportForms",
                column: "PMReportFormTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PMReportForms_ReportFormID",
                table: "PMReportForms",
                column: "ReportFormID");

            migrationBuilder.CreateIndex(
                name: "IX_PMReportForms_UpdatedBy",
                table: "PMReportForms",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMReportFormTypes_CreatedBy",
                table: "PMReportFormTypes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMReportFormTypes_UpdatedBy",
                table: "PMReportFormTypes",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMRTUCabinetCoolings_CreatedBy",
                table: "PMRTUCabinetCoolings",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMRTUCabinetCoolings_PMReportFormID",
                table: "PMRTUCabinetCoolings",
                column: "PMReportFormID");

            migrationBuilder.CreateIndex(
                name: "IX_PMRTUCabinetCoolings_UpdatedBy",
                table: "PMRTUCabinetCoolings",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_StationNameWarehouses_SystemNameWarehouseID",
                table: "StationNameWarehouses",
                column: "SystemNameWarehouseID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportForms_StationNameWarehouses_StationNameWarehouseID",
                table: "ReportForms",
                column: "StationNameWarehouseID",
                principalTable: "StationNameWarehouses",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportForms_SystemNameWarehouses_SystemNameWarehouseID",
                table: "ReportForms",
                column: "SystemNameWarehouseID",
                principalTable: "SystemNameWarehouses",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportForms_StationNameWarehouses_StationNameWarehouseID",
                table: "ReportForms");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportForms_SystemNameWarehouses_SystemNameWarehouseID",
                table: "ReportForms");

            migrationBuilder.DropTable(
                name: "PMChamberMagneticContacts");

            migrationBuilder.DropTable(
                name: "PMDVREquipments");

            migrationBuilder.DropTable(
                name: "PMMainRtuCabinets");

            migrationBuilder.DropTable(
                name: "PMRTUCabinetCoolings");

            migrationBuilder.DropTable(
                name: "StationNameWarehouses");

            migrationBuilder.DropTable(
                name: "PMReportForms");

            migrationBuilder.DropTable(
                name: "SystemNameWarehouses");

            migrationBuilder.DropTable(
                name: "PMReportFormTypes");

            migrationBuilder.DropIndex(
                name: "IX_ReportForms_StationNameWarehouseID",
                table: "ReportForms");

            migrationBuilder.DropIndex(
                name: "IX_ReportForms_SystemNameWarehouseID",
                table: "ReportForms");

            migrationBuilder.DropColumn(
                name: "JobNo",
                table: "ReportForms");

            migrationBuilder.DropColumn(
                name: "StationNameWarehouseID",
                table: "ReportForms");

            migrationBuilder.DropColumn(
                name: "SystemNameWarehouseID",
                table: "ReportForms");

            migrationBuilder.AddColumn<string>(
                name: "StationName",
                table: "CMReportForms",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemDescription",
                table: "CMReportForms",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }
    }
}
