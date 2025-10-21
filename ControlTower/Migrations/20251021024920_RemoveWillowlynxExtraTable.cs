using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class RemoveWillowlynxExtraTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PMServerWillowlynxCCTVCameras_WillowlynxCCTVCameraStatuses_WillowlynxCCTVCameraStatusID",
                table: "PMServerWillowlynxCCTVCameras");

            migrationBuilder.DropForeignKey(
                name: "FK_PMServerWillowlynxHistoricalReports_WillowlynxHistoricalReportStatuses_WillowlynxHistoricalReportStatusID",
                table: "PMServerWillowlynxHistoricalReports");

            migrationBuilder.DropForeignKey(
                name: "FK_PMServerWillowlynxHistoricalTrends_WillowlynxHistoricalTrendStatuses_WillowlynxHistoricalTrendStatusID",
                table: "PMServerWillowlynxHistoricalTrends");

            migrationBuilder.DropForeignKey(
                name: "FK_PMServerWillowlynxNetworkStatuses_WillowlynxNetworkStatuses_WillowlynxNetworkStatusID",
                table: "PMServerWillowlynxNetworkStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_PMServerWillowlynxRTUStatuses_WillowlynxRTUStatuses_WillowlynxRTUStatusID",
                table: "PMServerWillowlynxRTUStatuses");

            migrationBuilder.DropTable(
                name: "WillowlynxCCTVCameraStatuses");

            migrationBuilder.DropTable(
                name: "WillowlynxHistoricalReportStatuses");

            migrationBuilder.DropTable(
                name: "WillowlynxHistoricalTrendStatuses");

            migrationBuilder.DropTable(
                name: "WillowlynxNetworkStatuses");

            migrationBuilder.DropTable(
                name: "WillowlynxRTUStatuses");

            migrationBuilder.DropIndex(
                name: "IX_PMServerWillowlynxRTUStatuses_WillowlynxRTUStatusID",
                table: "PMServerWillowlynxRTUStatuses");

            migrationBuilder.DropIndex(
                name: "IX_PMServerWillowlynxNetworkStatuses_WillowlynxNetworkStatusID",
                table: "PMServerWillowlynxNetworkStatuses");

            migrationBuilder.DropIndex(
                name: "IX_PMServerWillowlynxHistoricalTrends_WillowlynxHistoricalTrendStatusID",
                table: "PMServerWillowlynxHistoricalTrends");

            migrationBuilder.DropIndex(
                name: "IX_PMServerWillowlynxHistoricalReports_WillowlynxHistoricalReportStatusID",
                table: "PMServerWillowlynxHistoricalReports");

            migrationBuilder.DropIndex(
                name: "IX_PMServerWillowlynxCCTVCameras_WillowlynxCCTVCameraStatusID",
                table: "PMServerWillowlynxCCTVCameras");

            migrationBuilder.DropColumn(
                name: "WillowlynxRTUStatusID",
                table: "PMServerWillowlynxRTUStatuses");

            migrationBuilder.DropColumn(
                name: "WillowlynxNetworkStatusID",
                table: "PMServerWillowlynxNetworkStatuses");

            migrationBuilder.DropColumn(
                name: "WillowlynxHistoricalTrendStatusID",
                table: "PMServerWillowlynxHistoricalTrends");

            migrationBuilder.DropColumn(
                name: "WillowlynxHistoricalReportStatusID",
                table: "PMServerWillowlynxHistoricalReports");

            migrationBuilder.DropColumn(
                name: "WillowlynxCCTVCameraStatusID",
                table: "PMServerWillowlynxCCTVCameras");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "PMServerHealthDetails",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WillowlynxRTUStatusID",
                table: "PMServerWillowlynxRTUStatuses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "WillowlynxNetworkStatusID",
                table: "PMServerWillowlynxNetworkStatuses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "WillowlynxHistoricalTrendStatusID",
                table: "PMServerWillowlynxHistoricalTrends",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "WillowlynxHistoricalReportStatusID",
                table: "PMServerWillowlynxHistoricalReports",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "WillowlynxCCTVCameraStatusID",
                table: "PMServerWillowlynxCCTVCameras",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "PMServerHealthDetails",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.CreateTable(
                name: "WillowlynxCCTVCameraStatuses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WillowlynxCCTVCameraStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WillowlynxHistoricalReportStatuses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WillowlynxHistoricalReportStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WillowlynxHistoricalTrendStatuses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WillowlynxHistoricalTrendStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WillowlynxNetworkStatuses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WillowlynxNetworkStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WillowlynxRTUStatuses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WillowlynxRTUStatuses", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxRTUStatuses_WillowlynxRTUStatusID",
                table: "PMServerWillowlynxRTUStatuses",
                column: "WillowlynxRTUStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxNetworkStatuses_WillowlynxNetworkStatusID",
                table: "PMServerWillowlynxNetworkStatuses",
                column: "WillowlynxNetworkStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxHistoricalTrends_WillowlynxHistoricalTrendStatusID",
                table: "PMServerWillowlynxHistoricalTrends",
                column: "WillowlynxHistoricalTrendStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxHistoricalReports_WillowlynxHistoricalReportStatusID",
                table: "PMServerWillowlynxHistoricalReports",
                column: "WillowlynxHistoricalReportStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxCCTVCameras_WillowlynxCCTVCameraStatusID",
                table: "PMServerWillowlynxCCTVCameras",
                column: "WillowlynxCCTVCameraStatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerWillowlynxCCTVCameras_WillowlynxCCTVCameraStatuses_WillowlynxCCTVCameraStatusID",
                table: "PMServerWillowlynxCCTVCameras",
                column: "WillowlynxCCTVCameraStatusID",
                principalTable: "WillowlynxCCTVCameraStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerWillowlynxHistoricalReports_WillowlynxHistoricalReportStatuses_WillowlynxHistoricalReportStatusID",
                table: "PMServerWillowlynxHistoricalReports",
                column: "WillowlynxHistoricalReportStatusID",
                principalTable: "WillowlynxHistoricalReportStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerWillowlynxHistoricalTrends_WillowlynxHistoricalTrendStatuses_WillowlynxHistoricalTrendStatusID",
                table: "PMServerWillowlynxHistoricalTrends",
                column: "WillowlynxHistoricalTrendStatusID",
                principalTable: "WillowlynxHistoricalTrendStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerWillowlynxNetworkStatuses_WillowlynxNetworkStatuses_WillowlynxNetworkStatusID",
                table: "PMServerWillowlynxNetworkStatuses",
                column: "WillowlynxNetworkStatusID",
                principalTable: "WillowlynxNetworkStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerWillowlynxRTUStatuses_WillowlynxRTUStatuses_WillowlynxRTUStatusID",
                table: "PMServerWillowlynxRTUStatuses",
                column: "WillowlynxRTUStatusID",
                principalTable: "WillowlynxRTUStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
