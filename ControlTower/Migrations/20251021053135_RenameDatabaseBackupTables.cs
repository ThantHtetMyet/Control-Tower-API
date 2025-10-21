using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class RenameDatabaseBackupTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PMServerMonthlyDatabaseBackupDetails");

            migrationBuilder.DropTable(
                name: "PMServerMonthlySCADADataBackupDetails");

            migrationBuilder.DropTable(
                name: "PMServerMonthlyDatabaseBackups");

            migrationBuilder.CreateTable(
                name: "PMServerDatabaseBackups",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormServerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    LatestBackupFileName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerDatabaseBackups", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerDatabaseBackups_PMReportFormServer_PMReportFormServerID",
                        column: x => x.PMReportFormServerID,
                        principalTable: "PMReportFormServer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerDatabaseBackups_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerDatabaseBackups_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerMSSQLDatabaseBackupDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMServerDatabaseBackupID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SerialNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ServerName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YesNoStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerMSSQLDatabaseBackupDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerMSSQLDatabaseBackupDetails_PMServerDatabaseBackups_PMServerDatabaseBackupID",
                        column: x => x.PMServerDatabaseBackupID,
                        principalTable: "PMServerDatabaseBackups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerMSSQLDatabaseBackupDetails_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerMSSQLDatabaseBackupDetails_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerMSSQLDatabaseBackupDetails_YesNoStatuses_YesNoStatusID",
                        column: x => x.YesNoStatusID,
                        principalTable: "YesNoStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerSCADADataBackupDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMServerDatabaseBackupID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SerialNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ServerName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YesNoStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerSCADADataBackupDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerSCADADataBackupDetails_PMServerDatabaseBackups_PMServerDatabaseBackupID",
                        column: x => x.PMServerDatabaseBackupID,
                        principalTable: "PMServerDatabaseBackups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerSCADADataBackupDetails_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerSCADADataBackupDetails_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerSCADADataBackupDetails_YesNoStatuses_YesNoStatusID",
                        column: x => x.YesNoStatusID,
                        principalTable: "YesNoStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PMServerDatabaseBackups_CreatedBy",
                table: "PMServerDatabaseBackups",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerDatabaseBackups_PMReportFormServerID",
                table: "PMServerDatabaseBackups",
                column: "PMReportFormServerID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerDatabaseBackups_UpdatedBy",
                table: "PMServerDatabaseBackups",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerMSSQLDatabaseBackupDetails_CreatedBy",
                table: "PMServerMSSQLDatabaseBackupDetails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerMSSQLDatabaseBackupDetails_PMServerDatabaseBackupID",
                table: "PMServerMSSQLDatabaseBackupDetails",
                column: "PMServerDatabaseBackupID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerMSSQLDatabaseBackupDetails_UpdatedBy",
                table: "PMServerMSSQLDatabaseBackupDetails",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerMSSQLDatabaseBackupDetails_YesNoStatusID",
                table: "PMServerMSSQLDatabaseBackupDetails",
                column: "YesNoStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerSCADADataBackupDetails_CreatedBy",
                table: "PMServerSCADADataBackupDetails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerSCADADataBackupDetails_PMServerDatabaseBackupID",
                table: "PMServerSCADADataBackupDetails",
                column: "PMServerDatabaseBackupID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerSCADADataBackupDetails_UpdatedBy",
                table: "PMServerSCADADataBackupDetails",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerSCADADataBackupDetails_YesNoStatusID",
                table: "PMServerSCADADataBackupDetails",
                column: "YesNoStatusID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PMServerMSSQLDatabaseBackupDetails");

            migrationBuilder.DropTable(
                name: "PMServerSCADADataBackupDetails");

            migrationBuilder.DropTable(
                name: "PMServerDatabaseBackups");

            migrationBuilder.CreateTable(
                name: "PMServerMonthlyDatabaseBackups",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormServerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LatestBackupFileName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerMonthlyDatabaseBackups", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerMonthlyDatabaseBackups_PMReportFormServer_PMReportFormServerID",
                        column: x => x.PMReportFormServerID,
                        principalTable: "PMReportFormServer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerMonthlyDatabaseBackups_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerMonthlyDatabaseBackups_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerMonthlyDatabaseBackupDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMServerMonthlyDatabaseBackupID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    YesNoStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SerialNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ServerName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerMonthlyDatabaseBackupDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerMonthlyDatabaseBackupDetails_PMServerMonthlyDatabaseBackups_PMServerMonthlyDatabaseBackupID",
                        column: x => x.PMServerMonthlyDatabaseBackupID,
                        principalTable: "PMServerMonthlyDatabaseBackups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerMonthlyDatabaseBackupDetails_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerMonthlyDatabaseBackupDetails_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerMonthlyDatabaseBackupDetails_YesNoStatuses_YesNoStatusID",
                        column: x => x.YesNoStatusID,
                        principalTable: "YesNoStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerMonthlySCADADataBackupDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMServerMonthlyDatabaseBackupID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    YesNoStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SerialNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ServerName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerMonthlySCADADataBackupDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerMonthlySCADADataBackupDetails_PMServerMonthlyDatabaseBackups_PMServerMonthlyDatabaseBackupID",
                        column: x => x.PMServerMonthlyDatabaseBackupID,
                        principalTable: "PMServerMonthlyDatabaseBackups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerMonthlySCADADataBackupDetails_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerMonthlySCADADataBackupDetails_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerMonthlySCADADataBackupDetails_YesNoStatuses_YesNoStatusID",
                        column: x => x.YesNoStatusID,
                        principalTable: "YesNoStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PMServerMonthlyDatabaseBackupDetails_CreatedBy",
                table: "PMServerMonthlyDatabaseBackupDetails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerMonthlyDatabaseBackupDetails_PMServerMonthlyDatabaseBackupID",
                table: "PMServerMonthlyDatabaseBackupDetails",
                column: "PMServerMonthlyDatabaseBackupID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerMonthlyDatabaseBackupDetails_UpdatedBy",
                table: "PMServerMonthlyDatabaseBackupDetails",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerMonthlyDatabaseBackupDetails_YesNoStatusID",
                table: "PMServerMonthlyDatabaseBackupDetails",
                column: "YesNoStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerMonthlyDatabaseBackups_CreatedBy",
                table: "PMServerMonthlyDatabaseBackups",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerMonthlyDatabaseBackups_PMReportFormServerID",
                table: "PMServerMonthlyDatabaseBackups",
                column: "PMReportFormServerID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerMonthlyDatabaseBackups_UpdatedBy",
                table: "PMServerMonthlyDatabaseBackups",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerMonthlySCADADataBackupDetails_CreatedBy",
                table: "PMServerMonthlySCADADataBackupDetails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerMonthlySCADADataBackupDetails_PMServerMonthlyDatabaseBackupID",
                table: "PMServerMonthlySCADADataBackupDetails",
                column: "PMServerMonthlyDatabaseBackupID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerMonthlySCADADataBackupDetails_UpdatedBy",
                table: "PMServerMonthlySCADADataBackupDetails",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerMonthlySCADADataBackupDetails_YesNoStatusID",
                table: "PMServerMonthlySCADADataBackupDetails",
                column: "YesNoStatusID");
        }
    }
}
