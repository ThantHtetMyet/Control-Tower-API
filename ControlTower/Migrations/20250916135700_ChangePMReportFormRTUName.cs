using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class ChangePMReportFormRTUName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PMChamberMagneticContacts_PMReportForms_PMReportFormID",
                table: "PMChamberMagneticContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_PMDVREquipments_PMReportForms_PMReportFormID",
                table: "PMDVREquipments");

            migrationBuilder.DropForeignKey(
                name: "FK_PMMainRtuCabinets_PMReportForms_PMReportFormID",
                table: "PMMainRtuCabinets");

            migrationBuilder.DropForeignKey(
                name: "FK_PMRTUCabinetCoolings_PMReportForms_PMReportFormID",
                table: "PMRTUCabinetCoolings");

            migrationBuilder.DropTable(
                name: "PMReportForms");

            migrationBuilder.CreateTable(
                name: "PMReportFormRTU",
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
                    table.PrimaryKey("PK_PMReportFormRTU", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMReportFormRTU_PMReportFormTypes_PMReportFormTypeID",
                        column: x => x.PMReportFormTypeID,
                        principalTable: "PMReportFormTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMReportFormRTU_ReportForms_ReportFormID",
                        column: x => x.ReportFormID,
                        principalTable: "ReportForms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMReportFormRTU_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMReportFormRTU_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PMReportFormRTU_CreatedBy",
                table: "PMReportFormRTU",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMReportFormRTU_PMReportFormTypeID",
                table: "PMReportFormRTU",
                column: "PMReportFormTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PMReportFormRTU_ReportFormID",
                table: "PMReportFormRTU",
                column: "ReportFormID");

            migrationBuilder.CreateIndex(
                name: "IX_PMReportFormRTU_UpdatedBy",
                table: "PMReportFormRTU",
                column: "UpdatedBy");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "PMReportFormRTU");

            migrationBuilder.CreateTable(
                name: "PMReportForms",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PMReportFormTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportFormID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttendedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CleaningOfCabinet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfService = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ProjectNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_PMChamberMagneticContacts_PMReportForms_PMReportFormID",
                table: "PMChamberMagneticContacts",
                column: "PMReportFormID",
                principalTable: "PMReportForms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMDVREquipments_PMReportForms_PMReportFormID",
                table: "PMDVREquipments",
                column: "PMReportFormID",
                principalTable: "PMReportForms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMMainRtuCabinets_PMReportForms_PMReportFormID",
                table: "PMMainRtuCabinets",
                column: "PMReportFormID",
                principalTable: "PMReportForms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMRTUCabinetCoolings_PMReportForms_PMReportFormID",
                table: "PMRTUCabinetCoolings",
                column: "PMReportFormID",
                principalTable: "PMReportForms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
