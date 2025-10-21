using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class AddPMServerSoftwarePatchDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPatch",
                table: "PMServerSoftwarePatchSummaries");

            migrationBuilder.DropColumn(
                name: "PreviousPatch",
                table: "PMServerSoftwarePatchSummaries");

            migrationBuilder.DropColumn(
                name: "SerialNo",
                table: "PMServerSoftwarePatchSummaries");

            migrationBuilder.DropColumn(
                name: "ServerName",
                table: "PMServerSoftwarePatchSummaries");

            migrationBuilder.CreateTable(
                name: "PMServerSoftwarePatchDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMServerSoftwarePatchSummaryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SerialNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ServerName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PreviousPatch = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CurrentPatch = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerSoftwarePatchDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerSoftwarePatchDetails_PMServerSoftwarePatchSummaries_PMServerSoftwarePatchSummaryID",
                        column: x => x.PMServerSoftwarePatchSummaryID,
                        principalTable: "PMServerSoftwarePatchSummaries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerSoftwarePatchDetails_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerSoftwarePatchDetails_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PMServerSoftwarePatchDetails_CreatedBy",
                table: "PMServerSoftwarePatchDetails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerSoftwarePatchDetails_PMServerSoftwarePatchSummaryID",
                table: "PMServerSoftwarePatchDetails",
                column: "PMServerSoftwarePatchSummaryID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerSoftwarePatchDetails_UpdatedBy",
                table: "PMServerSoftwarePatchDetails",
                column: "UpdatedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PMServerSoftwarePatchDetails");

            migrationBuilder.AddColumn<string>(
                name: "CurrentPatch",
                table: "PMServerSoftwarePatchSummaries",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousPatch",
                table: "PMServerSoftwarePatchSummaries",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SerialNo",
                table: "PMServerSoftwarePatchSummaries",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServerName",
                table: "PMServerSoftwarePatchSummaries",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}
