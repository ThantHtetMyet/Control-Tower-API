using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class CreateReportFormFinalReportTableFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportFormFinalReports",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportFormID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttachmentName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AttachmentPath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportFormFinalReports", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReportFormFinalReports_ReportForms_ReportFormID",
                        column: x => x.ReportFormID,
                        principalTable: "ReportForms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportFormFinalReports_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportFormFinalReports_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportFormFinalReports_Users_UploadedBy",
                        column: x => x.UploadedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportFormFinalReports_ReportFormID",
                table: "ReportFormFinalReports",
                column: "ReportFormID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFormFinalReports_CreatedBy",
                table: "ReportFormFinalReports",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFormFinalReports_UpdatedBy",
                table: "ReportFormFinalReports",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFormFinalReports_UploadedBy",
                table: "ReportFormFinalReports",
                column: "UploadedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportFormFinalReports");
        }
    }
}
