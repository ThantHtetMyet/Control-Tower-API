using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class AddPMServerReportFormPDFRequestLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PMServerReportFormPDFRequestLogs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormServerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerReportFormPDFRequestLogs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerReportFormPDFRequestLogs_PMReportFormServer_PMReportFormServerID",
                        column: x => x.PMReportFormServerID,
                        principalTable: "PMReportFormServer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PMServerReportFormPDFRequestLogs_Users_RequestedBy",
                        column: x => x.RequestedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PMServerReportFormPDFRequestLogs_PMReportFormServerID",
                table: "PMServerReportFormPDFRequestLogs",
                column: "PMReportFormServerID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerReportFormPDFRequestLogs_RequestedBy",
                table: "PMServerReportFormPDFRequestLogs",
                column: "RequestedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PMServerReportFormPDFRequestLogs");
        }
    }
}
