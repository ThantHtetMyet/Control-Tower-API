using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class AddRequiredTablesForPMServerReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ASAFirewallStatuses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CommandInput = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASAFirewallStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FailOverStatuses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FailOverStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NetworkStatuses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetworkStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PMReportFormServer",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportFormID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Customer = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReportTitle = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AttendedBy = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    WitnessedBy = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMReportFormServer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMReportFormServer_PMReportFormTypes_PMReportFormTypeID",
                        column: x => x.PMReportFormTypeID,
                        principalTable: "PMReportFormTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMReportFormServer_ReportForms_ReportFormID",
                        column: x => x.ReportFormID,
                        principalTable: "ReportForms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMReportFormServer_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMReportFormServer_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResultStatuses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ServerDiskStatuses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerDiskStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WillowlynxCCTVCameraStatuses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WillowlynxNetworkStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WillowlynxProcessStatuses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WillowlynxProcessStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WillowlynxRTUStatuses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WillowlynxRTUStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "YesNoStatuses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YesNoStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PMServerCPUAndMemoryUsages",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormServerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerCPUAndMemoryUsages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerCPUAndMemoryUsages_PMReportFormServer_PMReportFormServerID",
                        column: x => x.PMReportFormServerID,
                        principalTable: "PMReportFormServer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerCPUAndMemoryUsages_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerCPUAndMemoryUsages_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerDiskUsageHealths",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormServerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerDiskUsageHealths", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerDiskUsageHealths_PMReportFormServer_PMReportFormServerID",
                        column: x => x.PMReportFormServerID,
                        principalTable: "PMReportFormServer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerDiskUsageHealths_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerDiskUsageHealths_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerFailOvers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormServerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerFailOvers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerFailOvers_PMReportFormServer_PMReportFormServerID",
                        column: x => x.PMReportFormServerID,
                        principalTable: "PMReportFormServer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerFailOvers_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerFailOvers_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerHardDriveHealths",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormServerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerHardDriveHealths", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerHardDriveHealths_PMReportFormServer_PMReportFormServerID",
                        column: x => x.PMReportFormServerID,
                        principalTable: "PMReportFormServer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerHardDriveHealths_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerHardDriveHealths_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerHealths",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormServerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerHealths", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerHealths_PMReportFormServer_PMReportFormServerID",
                        column: x => x.PMReportFormServerID,
                        principalTable: "PMReportFormServer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerHealths_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerHealths_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerHotFixes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormServerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerHotFixes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerHotFixes_PMReportFormServer_PMReportFormServerID",
                        column: x => x.PMReportFormServerID,
                        principalTable: "PMReportFormServer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerHotFixes_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerHotFixes_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerMonthlyDatabaseBackups",
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
                name: "PMServerMonthlyDatabaseCreations",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormServerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerMonthlyDatabaseCreations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerMonthlyDatabaseCreations_PMReportFormServer_PMReportFormServerID",
                        column: x => x.PMReportFormServerID,
                        principalTable: "PMReportFormServer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerMonthlyDatabaseCreations_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerMonthlyDatabaseCreations_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerSoftwarePatchSummaries",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormServerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_PMServerSoftwarePatchSummaries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerSoftwarePatchSummaries_PMReportFormServer_PMReportFormServerID",
                        column: x => x.PMReportFormServerID,
                        principalTable: "PMReportFormServer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerSoftwarePatchSummaries_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerSoftwarePatchSummaries_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerTimeSyncs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormServerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerTimeSyncs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerTimeSyncs_PMReportFormServer_PMReportFormServerID",
                        column: x => x.PMReportFormServerID,
                        principalTable: "PMReportFormServer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerTimeSyncs_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerTimeSyncs_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerASAFirewalls",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormServerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ASAFirewallStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResultStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerASAFirewalls", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerASAFirewalls_ASAFirewallStatuses_ASAFirewallStatusID",
                        column: x => x.ASAFirewallStatusID,
                        principalTable: "ASAFirewallStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerASAFirewalls_PMReportFormServer_PMReportFormServerID",
                        column: x => x.PMReportFormServerID,
                        principalTable: "PMReportFormServer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerASAFirewalls_ResultStatuses_ResultStatusID",
                        column: x => x.ResultStatusID,
                        principalTable: "ResultStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerASAFirewalls_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerASAFirewalls_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerNetworkHealths",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormServerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NetworkStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YesNoStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateChecked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerNetworkHealths", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerNetworkHealths_NetworkStatuses_NetworkStatusID",
                        column: x => x.NetworkStatusID,
                        principalTable: "NetworkStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerNetworkHealths_PMReportFormServer_PMReportFormServerID",
                        column: x => x.PMReportFormServerID,
                        principalTable: "PMReportFormServer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerNetworkHealths_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerNetworkHealths_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerNetworkHealths_YesNoStatuses_YesNoStatusID",
                        column: x => x.YesNoStatusID,
                        principalTable: "YesNoStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerWillowlynxCCTVCameras",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormServerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WillowlynxCCTVCameraStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YesNoStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerWillowlynxCCTVCameras", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxCCTVCameras_PMReportFormServer_PMReportFormServerID",
                        column: x => x.PMReportFormServerID,
                        principalTable: "PMReportFormServer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxCCTVCameras_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxCCTVCameras_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxCCTVCameras_WillowlynxCCTVCameraStatuses_WillowlynxCCTVCameraStatusID",
                        column: x => x.WillowlynxCCTVCameraStatusID,
                        principalTable: "WillowlynxCCTVCameraStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxCCTVCameras_YesNoStatuses_YesNoStatusID",
                        column: x => x.YesNoStatusID,
                        principalTable: "YesNoStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerWillowlynxHistoricalReports",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormServerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WillowlynxHistoricalReportStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YesNoStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerWillowlynxHistoricalReports", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxHistoricalReports_PMReportFormServer_PMReportFormServerID",
                        column: x => x.PMReportFormServerID,
                        principalTable: "PMReportFormServer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxHistoricalReports_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxHistoricalReports_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxHistoricalReports_WillowlynxHistoricalReportStatuses_WillowlynxHistoricalReportStatusID",
                        column: x => x.WillowlynxHistoricalReportStatusID,
                        principalTable: "WillowlynxHistoricalReportStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxHistoricalReports_YesNoStatuses_YesNoStatusID",
                        column: x => x.YesNoStatusID,
                        principalTable: "YesNoStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerWillowlynxHistoricalTrends",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormServerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WillowlynxHistoricalTrendStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YesNoStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerWillowlynxHistoricalTrends", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxHistoricalTrends_PMReportFormServer_PMReportFormServerID",
                        column: x => x.PMReportFormServerID,
                        principalTable: "PMReportFormServer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxHistoricalTrends_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxHistoricalTrends_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxHistoricalTrends_WillowlynxHistoricalTrendStatuses_WillowlynxHistoricalTrendStatusID",
                        column: x => x.WillowlynxHistoricalTrendStatusID,
                        principalTable: "WillowlynxHistoricalTrendStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxHistoricalTrends_YesNoStatuses_YesNoStatusID",
                        column: x => x.YesNoStatusID,
                        principalTable: "YesNoStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerWillowlynxNetworkStatuses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormServerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WillowlynxNetworkStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YesNoStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerWillowlynxNetworkStatuses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxNetworkStatuses_PMReportFormServer_PMReportFormServerID",
                        column: x => x.PMReportFormServerID,
                        principalTable: "PMReportFormServer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxNetworkStatuses_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxNetworkStatuses_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxNetworkStatuses_WillowlynxNetworkStatuses_WillowlynxNetworkStatusID",
                        column: x => x.WillowlynxNetworkStatusID,
                        principalTable: "WillowlynxNetworkStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxNetworkStatuses_YesNoStatuses_YesNoStatusID",
                        column: x => x.YesNoStatusID,
                        principalTable: "YesNoStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerWillowlynxProcessStatuses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormServerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WillowlynxProcessStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YesNoStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerWillowlynxProcessStatuses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxProcessStatuses_PMReportFormServer_PMReportFormServerID",
                        column: x => x.PMReportFormServerID,
                        principalTable: "PMReportFormServer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxProcessStatuses_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxProcessStatuses_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxProcessStatuses_WillowlynxProcessStatuses_WillowlynxProcessStatusID",
                        column: x => x.WillowlynxProcessStatusID,
                        principalTable: "WillowlynxProcessStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxProcessStatuses_YesNoStatuses_YesNoStatusID",
                        column: x => x.YesNoStatusID,
                        principalTable: "YesNoStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerWillowlynxRTUStatuses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormServerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WillowlynxRTUStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YesNoStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerWillowlynxRTUStatuses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxRTUStatuses_PMReportFormServer_PMReportFormServerID",
                        column: x => x.PMReportFormServerID,
                        principalTable: "PMReportFormServer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxRTUStatuses_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxRTUStatuses_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxRTUStatuses_WillowlynxRTUStatuses_WillowlynxRTUStatusID",
                        column: x => x.WillowlynxRTUStatusID,
                        principalTable: "WillowlynxRTUStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerWillowlynxRTUStatuses_YesNoStatuses_YesNoStatusID",
                        column: x => x.YesNoStatusID,
                        principalTable: "YesNoStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerCPUUsageDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMServerCPUAndMemoryUsageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SerialNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ServerName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CPUUsage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ResultStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerCPUUsageDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerCPUUsageDetails_PMServerCPUAndMemoryUsages_PMServerCPUAndMemoryUsageID",
                        column: x => x.PMServerCPUAndMemoryUsageID,
                        principalTable: "PMServerCPUAndMemoryUsages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerCPUUsageDetails_ResultStatuses_ResultStatusID",
                        column: x => x.ResultStatusID,
                        principalTable: "ResultStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerCPUUsageDetails_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerCPUUsageDetails_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerMemoryUsageDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMServerCPUAndMemoryUsageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SerialNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ServerName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MemorySize = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MemoryInUse = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ResultStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerMemoryUsageDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerMemoryUsageDetails_PMServerCPUAndMemoryUsages_PMServerCPUAndMemoryUsageID",
                        column: x => x.PMServerCPUAndMemoryUsageID,
                        principalTable: "PMServerCPUAndMemoryUsages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerMemoryUsageDetails_ResultStatuses_ResultStatusID",
                        column: x => x.ResultStatusID,
                        principalTable: "ResultStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerMemoryUsageDetails_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerMemoryUsageDetails_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerDiskUsageHealthDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMServerDiskUsageHealthID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServerDiskStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResultStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiskName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ServerName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Capacity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FreeSpace = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Usage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerDiskUsageHealthDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerDiskUsageHealthDetails_PMServerDiskUsageHealths_PMServerDiskUsageHealthID",
                        column: x => x.PMServerDiskUsageHealthID,
                        principalTable: "PMServerDiskUsageHealths",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerDiskUsageHealthDetails_ResultStatuses_ResultStatusID",
                        column: x => x.ResultStatusID,
                        principalTable: "ResultStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerDiskUsageHealthDetails_ServerDiskStatuses_ServerDiskStatusID",
                        column: x => x.ServerDiskStatusID,
                        principalTable: "ServerDiskStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerDiskUsageHealthDetails_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerDiskUsageHealthDetails_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerFailOverDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMServerFailOverID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FailOverStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_PMServerFailOverDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerFailOverDetails_FailOverStatuses_FailOverStatusID",
                        column: x => x.FailOverStatusID,
                        principalTable: "FailOverStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerFailOverDetails_PMServerFailOvers_PMServerFailOverID",
                        column: x => x.PMServerFailOverID,
                        principalTable: "PMServerFailOvers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerFailOverDetails_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerFailOverDetails_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerFailOverDetails_YesNoStatuses_YesNoStatusID",
                        column: x => x.YesNoStatusID,
                        principalTable: "YesNoStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerHardDriveHealthDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMServerHardDriveHealthID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResultStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServerName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerHardDriveHealthDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerHardDriveHealthDetails_PMServerHardDriveHealths_PMServerHardDriveHealthID",
                        column: x => x.PMServerHardDriveHealthID,
                        principalTable: "PMServerHardDriveHealths",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerHardDriveHealthDetails_ResultStatuses_ResultStatusID",
                        column: x => x.ResultStatusID,
                        principalTable: "ResultStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerHardDriveHealthDetails_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerHardDriveHealthDetails_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerHealthDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMServerHealthID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResultStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServerName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerHealthDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerHealthDetails_PMServerHealths_PMServerHealthID",
                        column: x => x.PMServerHealthID,
                        principalTable: "PMServerHealths",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerHealthDetails_ResultStatuses_ResultStatusID",
                        column: x => x.ResultStatusID,
                        principalTable: "ResultStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerHealthDetails_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerHealthDetails_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerHotFixesDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMServerHotFixesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SerialNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ServerName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LatestHotFixsApplied = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ResultStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerHotFixesDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerHotFixesDetails_PMServerHotFixes_PMServerHotFixesID",
                        column: x => x.PMServerHotFixesID,
                        principalTable: "PMServerHotFixes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerHotFixesDetails_ResultStatuses_ResultStatusID",
                        column: x => x.ResultStatusID,
                        principalTable: "ResultStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerHotFixesDetails_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerHotFixesDetails_Users_UpdatedBy",
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
                    PMServerMonthlyDatabaseBackupID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    PMServerMonthlyDatabaseBackupID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "PMServerMonthlyDatabaseCreationDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMServerMonthlyDatabaseCreationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_PMServerMonthlyDatabaseCreationDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerMonthlyDatabaseCreationDetails_PMServerMonthlyDatabaseCreations_PMServerMonthlyDatabaseCreationID",
                        column: x => x.PMServerMonthlyDatabaseCreationID,
                        principalTable: "PMServerMonthlyDatabaseCreations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerMonthlyDatabaseCreationDetails_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerMonthlyDatabaseCreationDetails_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerMonthlyDatabaseCreationDetails_YesNoStatuses_YesNoStatusID",
                        column: x => x.YesNoStatusID,
                        principalTable: "YesNoStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerTimeSyncDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMServerTimeSyncID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SerialNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ServerName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ResultStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMServerTimeSyncDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerTimeSyncDetails_PMServerTimeSyncs_PMServerTimeSyncID",
                        column: x => x.PMServerTimeSyncID,
                        principalTable: "PMServerTimeSyncs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerTimeSyncDetails_ResultStatuses_ResultStatusID",
                        column: x => x.ResultStatusID,
                        principalTable: "ResultStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerTimeSyncDetails_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PMServerTimeSyncDetails_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PMReportFormServer_CreatedBy",
                table: "PMReportFormServer",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMReportFormServer_PMReportFormTypeID",
                table: "PMReportFormServer",
                column: "PMReportFormTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PMReportFormServer_ReportFormID",
                table: "PMReportFormServer",
                column: "ReportFormID");

            migrationBuilder.CreateIndex(
                name: "IX_PMReportFormServer_UpdatedBy",
                table: "PMReportFormServer",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerASAFirewalls_ASAFirewallStatusID",
                table: "PMServerASAFirewalls",
                column: "ASAFirewallStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerASAFirewalls_CreatedBy",
                table: "PMServerASAFirewalls",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerASAFirewalls_PMReportFormServerID",
                table: "PMServerASAFirewalls",
                column: "PMReportFormServerID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerASAFirewalls_ResultStatusID",
                table: "PMServerASAFirewalls",
                column: "ResultStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerASAFirewalls_UpdatedBy",
                table: "PMServerASAFirewalls",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerCPUAndMemoryUsages_CreatedBy",
                table: "PMServerCPUAndMemoryUsages",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerCPUAndMemoryUsages_PMReportFormServerID",
                table: "PMServerCPUAndMemoryUsages",
                column: "PMReportFormServerID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerCPUAndMemoryUsages_UpdatedBy",
                table: "PMServerCPUAndMemoryUsages",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerCPUUsageDetails_CreatedBy",
                table: "PMServerCPUUsageDetails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerCPUUsageDetails_PMServerCPUAndMemoryUsageID",
                table: "PMServerCPUUsageDetails",
                column: "PMServerCPUAndMemoryUsageID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerCPUUsageDetails_ResultStatusID",
                table: "PMServerCPUUsageDetails",
                column: "ResultStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerCPUUsageDetails_UpdatedBy",
                table: "PMServerCPUUsageDetails",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerDiskUsageHealthDetails_CreatedBy",
                table: "PMServerDiskUsageHealthDetails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerDiskUsageHealthDetails_PMServerDiskUsageHealthID",
                table: "PMServerDiskUsageHealthDetails",
                column: "PMServerDiskUsageHealthID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerDiskUsageHealthDetails_ResultStatusID",
                table: "PMServerDiskUsageHealthDetails",
                column: "ResultStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerDiskUsageHealthDetails_ServerDiskStatusID",
                table: "PMServerDiskUsageHealthDetails",
                column: "ServerDiskStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerDiskUsageHealthDetails_UpdatedBy",
                table: "PMServerDiskUsageHealthDetails",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerDiskUsageHealths_CreatedBy",
                table: "PMServerDiskUsageHealths",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerDiskUsageHealths_PMReportFormServerID",
                table: "PMServerDiskUsageHealths",
                column: "PMReportFormServerID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerDiskUsageHealths_UpdatedBy",
                table: "PMServerDiskUsageHealths",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerFailOverDetails_CreatedBy",
                table: "PMServerFailOverDetails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerFailOverDetails_FailOverStatusID",
                table: "PMServerFailOverDetails",
                column: "FailOverStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerFailOverDetails_PMServerFailOverID",
                table: "PMServerFailOverDetails",
                column: "PMServerFailOverID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerFailOverDetails_UpdatedBy",
                table: "PMServerFailOverDetails",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerFailOverDetails_YesNoStatusID",
                table: "PMServerFailOverDetails",
                column: "YesNoStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerFailOvers_CreatedBy",
                table: "PMServerFailOvers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerFailOvers_PMReportFormServerID",
                table: "PMServerFailOvers",
                column: "PMReportFormServerID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerFailOvers_UpdatedBy",
                table: "PMServerFailOvers",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerHardDriveHealthDetails_CreatedBy",
                table: "PMServerHardDriveHealthDetails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerHardDriveHealthDetails_PMServerHardDriveHealthID",
                table: "PMServerHardDriveHealthDetails",
                column: "PMServerHardDriveHealthID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerHardDriveHealthDetails_ResultStatusID",
                table: "PMServerHardDriveHealthDetails",
                column: "ResultStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerHardDriveHealthDetails_UpdatedBy",
                table: "PMServerHardDriveHealthDetails",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerHardDriveHealths_CreatedBy",
                table: "PMServerHardDriveHealths",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerHardDriveHealths_PMReportFormServerID",
                table: "PMServerHardDriveHealths",
                column: "PMReportFormServerID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerHardDriveHealths_UpdatedBy",
                table: "PMServerHardDriveHealths",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerHealthDetails_CreatedBy",
                table: "PMServerHealthDetails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerHealthDetails_PMServerHealthID",
                table: "PMServerHealthDetails",
                column: "PMServerHealthID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerHealthDetails_ResultStatusID",
                table: "PMServerHealthDetails",
                column: "ResultStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerHealthDetails_UpdatedBy",
                table: "PMServerHealthDetails",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerHealths_CreatedBy",
                table: "PMServerHealths",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerHealths_PMReportFormServerID",
                table: "PMServerHealths",
                column: "PMReportFormServerID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerHealths_UpdatedBy",
                table: "PMServerHealths",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerHotFixes_CreatedBy",
                table: "PMServerHotFixes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerHotFixes_PMReportFormServerID",
                table: "PMServerHotFixes",
                column: "PMReportFormServerID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerHotFixes_UpdatedBy",
                table: "PMServerHotFixes",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerHotFixesDetails_CreatedBy",
                table: "PMServerHotFixesDetails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerHotFixesDetails_PMServerHotFixesID",
                table: "PMServerHotFixesDetails",
                column: "PMServerHotFixesID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerHotFixesDetails_ResultStatusID",
                table: "PMServerHotFixesDetails",
                column: "ResultStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerHotFixesDetails_UpdatedBy",
                table: "PMServerHotFixesDetails",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerMemoryUsageDetails_CreatedBy",
                table: "PMServerMemoryUsageDetails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerMemoryUsageDetails_PMServerCPUAndMemoryUsageID",
                table: "PMServerMemoryUsageDetails",
                column: "PMServerCPUAndMemoryUsageID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerMemoryUsageDetails_ResultStatusID",
                table: "PMServerMemoryUsageDetails",
                column: "ResultStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerMemoryUsageDetails_UpdatedBy",
                table: "PMServerMemoryUsageDetails",
                column: "UpdatedBy");

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
                name: "IX_PMServerMonthlyDatabaseCreationDetails_CreatedBy",
                table: "PMServerMonthlyDatabaseCreationDetails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerMonthlyDatabaseCreationDetails_PMServerMonthlyDatabaseCreationID",
                table: "PMServerMonthlyDatabaseCreationDetails",
                column: "PMServerMonthlyDatabaseCreationID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerMonthlyDatabaseCreationDetails_UpdatedBy",
                table: "PMServerMonthlyDatabaseCreationDetails",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerMonthlyDatabaseCreationDetails_YesNoStatusID",
                table: "PMServerMonthlyDatabaseCreationDetails",
                column: "YesNoStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerMonthlyDatabaseCreations_CreatedBy",
                table: "PMServerMonthlyDatabaseCreations",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerMonthlyDatabaseCreations_PMReportFormServerID",
                table: "PMServerMonthlyDatabaseCreations",
                column: "PMReportFormServerID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerMonthlyDatabaseCreations_UpdatedBy",
                table: "PMServerMonthlyDatabaseCreations",
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

            migrationBuilder.CreateIndex(
                name: "IX_PMServerNetworkHealths_CreatedBy",
                table: "PMServerNetworkHealths",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerNetworkHealths_NetworkStatusID",
                table: "PMServerNetworkHealths",
                column: "NetworkStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerNetworkHealths_PMReportFormServerID",
                table: "PMServerNetworkHealths",
                column: "PMReportFormServerID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerNetworkHealths_UpdatedBy",
                table: "PMServerNetworkHealths",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerNetworkHealths_YesNoStatusID",
                table: "PMServerNetworkHealths",
                column: "YesNoStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerSoftwarePatchSummaries_CreatedBy",
                table: "PMServerSoftwarePatchSummaries",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerSoftwarePatchSummaries_PMReportFormServerID",
                table: "PMServerSoftwarePatchSummaries",
                column: "PMReportFormServerID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerSoftwarePatchSummaries_UpdatedBy",
                table: "PMServerSoftwarePatchSummaries",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerTimeSyncDetails_CreatedBy",
                table: "PMServerTimeSyncDetails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerTimeSyncDetails_PMServerTimeSyncID",
                table: "PMServerTimeSyncDetails",
                column: "PMServerTimeSyncID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerTimeSyncDetails_ResultStatusID",
                table: "PMServerTimeSyncDetails",
                column: "ResultStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerTimeSyncDetails_UpdatedBy",
                table: "PMServerTimeSyncDetails",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerTimeSyncs_CreatedBy",
                table: "PMServerTimeSyncs",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerTimeSyncs_PMReportFormServerID",
                table: "PMServerTimeSyncs",
                column: "PMReportFormServerID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerTimeSyncs_UpdatedBy",
                table: "PMServerTimeSyncs",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxCCTVCameras_CreatedBy",
                table: "PMServerWillowlynxCCTVCameras",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxCCTVCameras_PMReportFormServerID",
                table: "PMServerWillowlynxCCTVCameras",
                column: "PMReportFormServerID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxCCTVCameras_UpdatedBy",
                table: "PMServerWillowlynxCCTVCameras",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxCCTVCameras_WillowlynxCCTVCameraStatusID",
                table: "PMServerWillowlynxCCTVCameras",
                column: "WillowlynxCCTVCameraStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxCCTVCameras_YesNoStatusID",
                table: "PMServerWillowlynxCCTVCameras",
                column: "YesNoStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxHistoricalReports_CreatedBy",
                table: "PMServerWillowlynxHistoricalReports",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxHistoricalReports_PMReportFormServerID",
                table: "PMServerWillowlynxHistoricalReports",
                column: "PMReportFormServerID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxHistoricalReports_UpdatedBy",
                table: "PMServerWillowlynxHistoricalReports",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxHistoricalReports_WillowlynxHistoricalReportStatusID",
                table: "PMServerWillowlynxHistoricalReports",
                column: "WillowlynxHistoricalReportStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxHistoricalReports_YesNoStatusID",
                table: "PMServerWillowlynxHistoricalReports",
                column: "YesNoStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxHistoricalTrends_CreatedBy",
                table: "PMServerWillowlynxHistoricalTrends",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxHistoricalTrends_PMReportFormServerID",
                table: "PMServerWillowlynxHistoricalTrends",
                column: "PMReportFormServerID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxHistoricalTrends_UpdatedBy",
                table: "PMServerWillowlynxHistoricalTrends",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxHistoricalTrends_WillowlynxHistoricalTrendStatusID",
                table: "PMServerWillowlynxHistoricalTrends",
                column: "WillowlynxHistoricalTrendStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxHistoricalTrends_YesNoStatusID",
                table: "PMServerWillowlynxHistoricalTrends",
                column: "YesNoStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxNetworkStatuses_CreatedBy",
                table: "PMServerWillowlynxNetworkStatuses",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxNetworkStatuses_PMReportFormServerID",
                table: "PMServerWillowlynxNetworkStatuses",
                column: "PMReportFormServerID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxNetworkStatuses_UpdatedBy",
                table: "PMServerWillowlynxNetworkStatuses",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxNetworkStatuses_WillowlynxNetworkStatusID",
                table: "PMServerWillowlynxNetworkStatuses",
                column: "WillowlynxNetworkStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxNetworkStatuses_YesNoStatusID",
                table: "PMServerWillowlynxNetworkStatuses",
                column: "YesNoStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxProcessStatuses_CreatedBy",
                table: "PMServerWillowlynxProcessStatuses",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxProcessStatuses_PMReportFormServerID",
                table: "PMServerWillowlynxProcessStatuses",
                column: "PMReportFormServerID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxProcessStatuses_UpdatedBy",
                table: "PMServerWillowlynxProcessStatuses",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxProcessStatuses_WillowlynxProcessStatusID",
                table: "PMServerWillowlynxProcessStatuses",
                column: "WillowlynxProcessStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxProcessStatuses_YesNoStatusID",
                table: "PMServerWillowlynxProcessStatuses",
                column: "YesNoStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxRTUStatuses_CreatedBy",
                table: "PMServerWillowlynxRTUStatuses",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxRTUStatuses_PMReportFormServerID",
                table: "PMServerWillowlynxRTUStatuses",
                column: "PMReportFormServerID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxRTUStatuses_UpdatedBy",
                table: "PMServerWillowlynxRTUStatuses",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxRTUStatuses_WillowlynxRTUStatusID",
                table: "PMServerWillowlynxRTUStatuses",
                column: "WillowlynxRTUStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxRTUStatuses_YesNoStatusID",
                table: "PMServerWillowlynxRTUStatuses",
                column: "YesNoStatusID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PMServerASAFirewalls");

            migrationBuilder.DropTable(
                name: "PMServerCPUUsageDetails");

            migrationBuilder.DropTable(
                name: "PMServerDiskUsageHealthDetails");

            migrationBuilder.DropTable(
                name: "PMServerFailOverDetails");

            migrationBuilder.DropTable(
                name: "PMServerHardDriveHealthDetails");

            migrationBuilder.DropTable(
                name: "PMServerHealthDetails");

            migrationBuilder.DropTable(
                name: "PMServerHotFixesDetails");

            migrationBuilder.DropTable(
                name: "PMServerMemoryUsageDetails");

            migrationBuilder.DropTable(
                name: "PMServerMonthlyDatabaseBackupDetails");

            migrationBuilder.DropTable(
                name: "PMServerMonthlyDatabaseCreationDetails");

            migrationBuilder.DropTable(
                name: "PMServerMonthlySCADADataBackupDetails");

            migrationBuilder.DropTable(
                name: "PMServerNetworkHealths");

            migrationBuilder.DropTable(
                name: "PMServerSoftwarePatchSummaries");

            migrationBuilder.DropTable(
                name: "PMServerTimeSyncDetails");

            migrationBuilder.DropTable(
                name: "PMServerWillowlynxCCTVCameras");

            migrationBuilder.DropTable(
                name: "PMServerWillowlynxHistoricalReports");

            migrationBuilder.DropTable(
                name: "PMServerWillowlynxHistoricalTrends");

            migrationBuilder.DropTable(
                name: "PMServerWillowlynxNetworkStatuses");

            migrationBuilder.DropTable(
                name: "PMServerWillowlynxProcessStatuses");

            migrationBuilder.DropTable(
                name: "PMServerWillowlynxRTUStatuses");

            migrationBuilder.DropTable(
                name: "ASAFirewallStatuses");

            migrationBuilder.DropTable(
                name: "PMServerDiskUsageHealths");

            migrationBuilder.DropTable(
                name: "ServerDiskStatuses");

            migrationBuilder.DropTable(
                name: "FailOverStatuses");

            migrationBuilder.DropTable(
                name: "PMServerFailOvers");

            migrationBuilder.DropTable(
                name: "PMServerHardDriveHealths");

            migrationBuilder.DropTable(
                name: "PMServerHealths");

            migrationBuilder.DropTable(
                name: "PMServerHotFixes");

            migrationBuilder.DropTable(
                name: "PMServerCPUAndMemoryUsages");

            migrationBuilder.DropTable(
                name: "PMServerMonthlyDatabaseCreations");

            migrationBuilder.DropTable(
                name: "PMServerMonthlyDatabaseBackups");

            migrationBuilder.DropTable(
                name: "NetworkStatuses");

            migrationBuilder.DropTable(
                name: "PMServerTimeSyncs");

            migrationBuilder.DropTable(
                name: "ResultStatuses");

            migrationBuilder.DropTable(
                name: "WillowlynxCCTVCameraStatuses");

            migrationBuilder.DropTable(
                name: "WillowlynxHistoricalReportStatuses");

            migrationBuilder.DropTable(
                name: "WillowlynxHistoricalTrendStatuses");

            migrationBuilder.DropTable(
                name: "WillowlynxNetworkStatuses");

            migrationBuilder.DropTable(
                name: "WillowlynxProcessStatuses");

            migrationBuilder.DropTable(
                name: "WillowlynxRTUStatuses");

            migrationBuilder.DropTable(
                name: "YesNoStatuses");

            migrationBuilder.DropTable(
                name: "PMReportFormServer");
        }
    }
}
