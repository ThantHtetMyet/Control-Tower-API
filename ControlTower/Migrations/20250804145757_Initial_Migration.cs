using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessLevels",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessLevels", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ActionTaken",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceReportFormID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionTaken", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FollowupActionWarehouses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FollowupActionNo = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowupActionWarehouses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FormStatus",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormStatusWarehouseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceReportFormID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FormStatusWarehouses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormStatusWarehouses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FurtherActionTaken",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FurtherActionTakenWarehouseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceReportFormID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FurtherActionTaken", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FurtherActionTakenWarehouses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FurtherActionTakenWarehouses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ImportFileRecords",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImportFormTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    StoredDirectory = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    ImportedStatus = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    UploadedStatus = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "Date", nullable: true),
                    UploadedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportFileRecords", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ImportFormTypes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportFormTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "IssueFound",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceReportFormID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueFound", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "IssueReported",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceReportFormID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueReported", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LocationWarehouses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationWarehouses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Occupations",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OccupationName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Occupations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OccupationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffCardID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StaffRFIDCardID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    LoginPassword = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartWorkingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastWorkingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkPermit = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Religion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkPassCardNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WorkPassCardIssuedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkPassCardExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Occupations_OccupationID",
                        column: x => x.OccupationID,
                        principalTable: "Occupations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectNoWarehouses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectNoWarehouses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProjectNoWarehouses_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectNoWarehouses_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTypeWarehouses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypeWarehouses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ServiceTypeWarehouses_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceTypeWarehouses_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemWarehouses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemWarehouses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SystemWarehouses_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SystemWarehouses_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserApplicationAccesses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessLevelID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GrantedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    RevokedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GrantedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserApplicationAccesses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserApplicationAccesses_AccessLevels_AccessLevelID",
                        column: x => x.AccessLevelID,
                        principalTable: "AccessLevels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserApplicationAccesses_Applications_ApplicationID",
                        column: x => x.ApplicationID,
                        principalTable: "Applications",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserApplicationAccesses_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserApplicationAccesses_Users_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserApplicationAccesses_Users_GrantedBy",
                        column: x => x.GrantedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserApplicationAccesses_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceReportForms",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectNoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SystemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FollowupActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FailureDetectedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResponseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceReportForms", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ServiceReportForms_FollowupActionWarehouses_FollowupActionID",
                        column: x => x.FollowupActionID,
                        principalTable: "FollowupActionWarehouses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceReportForms_LocationWarehouses_LocationID",
                        column: x => x.LocationID,
                        principalTable: "LocationWarehouses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceReportForms_ProjectNoWarehouses_ProjectNoID",
                        column: x => x.ProjectNoID,
                        principalTable: "ProjectNoWarehouses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceReportForms_SystemWarehouses_SystemID",
                        column: x => x.SystemID,
                        principalTable: "SystemWarehouses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceReportForms_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceReportForms_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceType",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceTypeWarehouseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceReportFormID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceType", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ServiceType_ServiceReportForms_ServiceReportFormID",
                        column: x => x.ServiceReportFormID,
                        principalTable: "ServiceReportForms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceType_ServiceTypeWarehouses_ServiceTypeWarehouseID",
                        column: x => x.ServiceTypeWarehouseID,
                        principalTable: "ServiceTypeWarehouses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceType_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessLevels_CreatedBy",
                table: "AccessLevels",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AccessLevels_LevelName",
                table: "AccessLevels",
                column: "LevelName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccessLevels_UpdatedBy",
                table: "AccessLevels",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ActionTaken_CreatedBy",
                table: "ActionTaken",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ActionTaken_ServiceReportFormID",
                table: "ActionTaken",
                column: "ServiceReportFormID");

            migrationBuilder.CreateIndex(
                name: "IX_ActionTaken_UpdatedBy",
                table: "ActionTaken",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ApplicationName",
                table: "Applications",
                column: "ApplicationName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_CreatedBy",
                table: "Applications",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_UpdatedBy",
                table: "Applications",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CreatedBy",
                table: "Departments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Name",
                table: "Departments",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_UpdatedBy",
                table: "Departments",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_FollowupActionWarehouses_CreatedBy",
                table: "FollowupActionWarehouses",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_FollowupActionWarehouses_UpdatedBy",
                table: "FollowupActionWarehouses",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_FormStatus_CreatedBy",
                table: "FormStatus",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_FormStatus_FormStatusWarehouseID",
                table: "FormStatus",
                column: "FormStatusWarehouseID");

            migrationBuilder.CreateIndex(
                name: "IX_FormStatus_ServiceReportFormID",
                table: "FormStatus",
                column: "ServiceReportFormID");

            migrationBuilder.CreateIndex(
                name: "IX_FormStatus_UpdatedBy",
                table: "FormStatus",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_FormStatusWarehouses_CreatedBy",
                table: "FormStatusWarehouses",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_FormStatusWarehouses_UpdatedBy",
                table: "FormStatusWarehouses",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_FurtherActionTaken_CreatedBy",
                table: "FurtherActionTaken",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_FurtherActionTaken_FurtherActionTakenWarehouseID",
                table: "FurtherActionTaken",
                column: "FurtherActionTakenWarehouseID");

            migrationBuilder.CreateIndex(
                name: "IX_FurtherActionTaken_ServiceReportFormID",
                table: "FurtherActionTaken",
                column: "ServiceReportFormID");

            migrationBuilder.CreateIndex(
                name: "IX_FurtherActionTaken_UpdatedBy",
                table: "FurtherActionTaken",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_FurtherActionTakenWarehouses_CreatedBy",
                table: "FurtherActionTakenWarehouses",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_FurtherActionTakenWarehouses_UpdatedBy",
                table: "FurtherActionTakenWarehouses",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ImportFileRecords_CreatedBy",
                table: "ImportFileRecords",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ImportFileRecords_ImportFormTypeID",
                table: "ImportFileRecords",
                column: "ImportFormTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ImportFileRecords_UpdatedBy",
                table: "ImportFileRecords",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ImportFileRecords_UploadedBy",
                table: "ImportFileRecords",
                column: "UploadedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ImportFormTypes_CreatedBy",
                table: "ImportFormTypes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ImportFormTypes_UpdatedBy",
                table: "ImportFormTypes",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_IssueFound_CreatedBy",
                table: "IssueFound",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_IssueFound_ServiceReportFormID",
                table: "IssueFound",
                column: "ServiceReportFormID");

            migrationBuilder.CreateIndex(
                name: "IX_IssueFound_UpdatedBy",
                table: "IssueFound",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_IssueReported_CreatedBy",
                table: "IssueReported",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_IssueReported_ServiceReportFormID",
                table: "IssueReported",
                column: "ServiceReportFormID");

            migrationBuilder.CreateIndex(
                name: "IX_IssueReported_UpdatedBy",
                table: "IssueReported",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LocationWarehouses_CreatedBy",
                table: "LocationWarehouses",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LocationWarehouses_UpdatedBy",
                table: "LocationWarehouses",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Occupations_CreatedBy",
                table: "Occupations",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Occupations_OccupationName",
                table: "Occupations",
                column: "OccupationName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Occupations_UpdatedBy",
                table: "Occupations",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectNoWarehouses_CreatedBy",
                table: "ProjectNoWarehouses",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectNoWarehouses_UpdatedBy",
                table: "ProjectNoWarehouses",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReportForms_CreatedBy",
                table: "ServiceReportForms",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReportForms_FollowupActionID",
                table: "ServiceReportForms",
                column: "FollowupActionID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReportForms_LocationID",
                table: "ServiceReportForms",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReportForms_ProjectNoID",
                table: "ServiceReportForms",
                column: "ProjectNoID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReportForms_SystemID",
                table: "ServiceReportForms",
                column: "SystemID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReportForms_UpdatedBy",
                table: "ServiceReportForms",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceType_ServiceReportFormID",
                table: "ServiceType",
                column: "ServiceReportFormID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceType_ServiceTypeWarehouseID",
                table: "ServiceType",
                column: "ServiceTypeWarehouseID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceType_UpdatedBy",
                table: "ServiceType",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTypeWarehouses_CreatedBy",
                table: "ServiceTypeWarehouses",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTypeWarehouses_UpdatedBy",
                table: "ServiceTypeWarehouses",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SystemWarehouses_CreatedBy",
                table: "SystemWarehouses",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SystemWarehouses_UpdatedBy",
                table: "SystemWarehouses",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserApplicationAccesses_AccessLevelID",
                table: "UserApplicationAccesses",
                column: "AccessLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_UserApplicationAccesses_ApplicationID",
                table: "UserApplicationAccesses",
                column: "ApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_UserApplicationAccesses_CreatedBy",
                table: "UserApplicationAccesses",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserApplicationAccesses_EmployeeID_ApplicationID",
                table: "UserApplicationAccesses",
                columns: new[] { "EmployeeID", "ApplicationID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserApplicationAccesses_GrantedBy",
                table: "UserApplicationAccesses",
                column: "GrantedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserApplicationAccesses_UpdatedBy",
                table: "UserApplicationAccesses",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CreatedBy",
                table: "Users",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentID",
                table: "Users",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_OccupationID",
                table: "Users",
                column: "OccupationID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StaffCardID",
                table: "Users",
                column: "StaffCardID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UpdatedBy",
                table: "Users",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessLevels_Users_CreatedBy",
                table: "AccessLevels",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessLevels_Users_UpdatedBy",
                table: "AccessLevels",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ActionTaken_ServiceReportForms_ServiceReportFormID",
                table: "ActionTaken",
                column: "ServiceReportFormID",
                principalTable: "ServiceReportForms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActionTaken_Users_CreatedBy",
                table: "ActionTaken",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionTaken_Users_UpdatedBy",
                table: "ActionTaken",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Users_CreatedBy",
                table: "Applications",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Users_UpdatedBy",
                table: "Applications",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Users_CreatedBy",
                table: "Departments",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Users_UpdatedBy",
                table: "Departments",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowupActionWarehouses_Users_CreatedBy",
                table: "FollowupActionWarehouses",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowupActionWarehouses_Users_UpdatedBy",
                table: "FollowupActionWarehouses",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FormStatus_FormStatusWarehouses_FormStatusWarehouseID",
                table: "FormStatus",
                column: "FormStatusWarehouseID",
                principalTable: "FormStatusWarehouses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormStatus_ServiceReportForms_ServiceReportFormID",
                table: "FormStatus",
                column: "ServiceReportFormID",
                principalTable: "ServiceReportForms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormStatus_Users_CreatedBy",
                table: "FormStatus",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FormStatus_Users_UpdatedBy",
                table: "FormStatus",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FormStatusWarehouses_Users_CreatedBy",
                table: "FormStatusWarehouses",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FormStatusWarehouses_Users_UpdatedBy",
                table: "FormStatusWarehouses",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FurtherActionTaken_FurtherActionTakenWarehouses_FurtherActionTakenWarehouseID",
                table: "FurtherActionTaken",
                column: "FurtherActionTakenWarehouseID",
                principalTable: "FurtherActionTakenWarehouses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FurtherActionTaken_ServiceReportForms_ServiceReportFormID",
                table: "FurtherActionTaken",
                column: "ServiceReportFormID",
                principalTable: "ServiceReportForms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FurtherActionTaken_Users_CreatedBy",
                table: "FurtherActionTaken",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FurtherActionTaken_Users_UpdatedBy",
                table: "FurtherActionTaken",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FurtherActionTakenWarehouses_Users_CreatedBy",
                table: "FurtherActionTakenWarehouses",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FurtherActionTakenWarehouses_Users_UpdatedBy",
                table: "FurtherActionTakenWarehouses",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ImportFileRecords_ImportFormTypes_ImportFormTypeID",
                table: "ImportFileRecords",
                column: "ImportFormTypeID",
                principalTable: "ImportFormTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ImportFileRecords_Users_CreatedBy",
                table: "ImportFileRecords",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ImportFileRecords_Users_UpdatedBy",
                table: "ImportFileRecords",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ImportFileRecords_Users_UploadedBy",
                table: "ImportFileRecords",
                column: "UploadedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ImportFormTypes_Users_CreatedBy",
                table: "ImportFormTypes",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ImportFormTypes_Users_UpdatedBy",
                table: "ImportFormTypes",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueFound_ServiceReportForms_ServiceReportFormID",
                table: "IssueFound",
                column: "ServiceReportFormID",
                principalTable: "ServiceReportForms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueFound_Users_CreatedBy",
                table: "IssueFound",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueFound_Users_UpdatedBy",
                table: "IssueFound",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueReported_ServiceReportForms_ServiceReportFormID",
                table: "IssueReported",
                column: "ServiceReportFormID",
                principalTable: "ServiceReportForms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueReported_Users_CreatedBy",
                table: "IssueReported",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueReported_Users_UpdatedBy",
                table: "IssueReported",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationWarehouses_Users_CreatedBy",
                table: "LocationWarehouses",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationWarehouses_Users_UpdatedBy",
                table: "LocationWarehouses",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Occupations_Users_CreatedBy",
                table: "Occupations",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Occupations_Users_UpdatedBy",
                table: "Occupations",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Users_CreatedBy",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Users_UpdatedBy",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Occupations_Users_CreatedBy",
                table: "Occupations");

            migrationBuilder.DropForeignKey(
                name: "FK_Occupations_Users_UpdatedBy",
                table: "Occupations");

            migrationBuilder.DropTable(
                name: "ActionTaken");

            migrationBuilder.DropTable(
                name: "FormStatus");

            migrationBuilder.DropTable(
                name: "FurtherActionTaken");

            migrationBuilder.DropTable(
                name: "ImportFileRecords");

            migrationBuilder.DropTable(
                name: "IssueFound");

            migrationBuilder.DropTable(
                name: "IssueReported");

            migrationBuilder.DropTable(
                name: "ServiceType");

            migrationBuilder.DropTable(
                name: "UserApplicationAccesses");

            migrationBuilder.DropTable(
                name: "FormStatusWarehouses");

            migrationBuilder.DropTable(
                name: "FurtherActionTakenWarehouses");

            migrationBuilder.DropTable(
                name: "ImportFormTypes");

            migrationBuilder.DropTable(
                name: "ServiceReportForms");

            migrationBuilder.DropTable(
                name: "ServiceTypeWarehouses");

            migrationBuilder.DropTable(
                name: "AccessLevels");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "FollowupActionWarehouses");

            migrationBuilder.DropTable(
                name: "LocationWarehouses");

            migrationBuilder.DropTable(
                name: "ProjectNoWarehouses");

            migrationBuilder.DropTable(
                name: "SystemWarehouses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Occupations");
        }
    }
}
