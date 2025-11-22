using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
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
                name: "RoomBookingStatus",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomBookingStatus", x => x.ID);
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
                name: "Buildings",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CMMaterialUsed",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CMReportFormID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewSerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldSerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CMMaterialUsed", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CMReportForms",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportFormID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CMReportFormTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FurtherActionTakenID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormstatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Customer = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReportTitle = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProjectNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IssueReportedDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IssueFoundDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ActionTakenDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    FailureDetectedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResponseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AttendedBy = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ApprovedBy = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CMReportForms", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CMReportFormTypes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CMReportFormTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.ID);
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
                name: "FormStatusWarehouses",
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
                    table.PrimaryKey("PK_FormStatusWarehouses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FurtherActionTakenWarehouses",
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
                    table.PrimaryKey("PK_FurtherActionTakenWarehouses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ImageTypes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageTypeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ImportFileRecords",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImportFormTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StoredDirectory = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImportedStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UploadedStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UploadedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false),
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
                name: "News",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Excerpt = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NewsCategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: true),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NewsCategory",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ParentCategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsCategory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NewsCategory_NewsCategory_ParentCategoryID",
                        column: x => x.ParentCategoryID,
                        principalTable: "NewsCategory",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NewsComments",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentCommentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsComments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NewsComments_NewsComments_ParentCommentID",
                        column: x => x.ParentCommentID,
                        principalTable: "NewsComments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NewsComments_News_NewsID",
                        column: x => x.NewsID,
                        principalTable: "News",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsImages",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StoredDirectory = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UploadedStatus = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AltText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ImageType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsImages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NewsImages_News_NewsID",
                        column: x => x.NewsID,
                        principalTable: "News",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsReactions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReactionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsReactions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NewsReactions_News_NewsID",
                        column: x => x.NewsID,
                        principalTable: "News",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OccupationLevels",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Rank = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OccupationLevels", x => x.ID);
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
                    OccupationLevelID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Occupations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Occupations_OccupationLevels_OccupationLevelID",
                        column: x => x.OccupationLevelID,
                        principalTable: "OccupationLevels",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PMChamberMagneticContacts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormRTUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChamberNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChamberOGBox = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChamberContact1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChamberContact2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChamberContact3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMChamberMagneticContacts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PMDVREquipments",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormRTUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "PMMainRtuCabinets",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormRTUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                });

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
                    FormstatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        name: "FK_PMReportFormRTU_FormStatusWarehouses_FormstatusID",
                        column: x => x.FormstatusID,
                        principalTable: "FormStatusWarehouses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
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
                    FormstatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        name: "FK_PMReportFormServer_FormStatusWarehouses_FormstatusID",
                        column: x => x.FormstatusID,
                        principalTable: "FormStatusWarehouses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

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
                });

            migrationBuilder.CreateTable(
                name: "PMRTUCabinetCoolings",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormRTUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FanNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FunctionalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                        name: "FK_PMRTUCabinetCoolings_PMReportFormRTU_PMReportFormRTUID",
                        column: x => x.PMReportFormRTUID,
                        principalTable: "PMReportFormRTU",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerASAFirewalls",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormServerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SerialNumber = table.Column<int>(type: "int", nullable: false),
                    CommandInput = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ASAFirewallStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                });

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
                });

            migrationBuilder.CreateTable(
                name: "PMServerFailOverDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMServerFailOverID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YesNoStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToServer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromServer = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                        name: "FK_PMServerFailOverDetails_YesNoStatuses_YesNoStatusID",
                        column: x => x.YesNoStatusID,
                        principalTable: "YesNoStatuses",
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
                        name: "FK_PMServerHardDriveHealthDetails_ResultStatuses_ResultStatusID",
                        column: x => x.ResultStatusID,
                        principalTable: "ResultStatuses",
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
                        name: "FK_PMServerHealthDetails_ResultStatuses_ResultStatusID",
                        column: x => x.ResultStatusID,
                        principalTable: "ResultStatuses",
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
                        name: "FK_PMServerMonthlyDatabaseCreationDetails_YesNoStatuses_YesNoStatusID",
                        column: x => x.YesNoStatusID,
                        principalTable: "YesNoStatuses",
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
                        name: "FK_PMServerMSSQLDatabaseBackupDetails_YesNoStatuses_YesNoStatusID",
                        column: x => x.YesNoStatusID,
                        principalTable: "YesNoStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PMServerNetworkHealths",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormServerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        name: "FK_PMServerNetworkHealths_PMReportFormServer_PMReportFormServerID",
                        column: x => x.PMReportFormServerID,
                        principalTable: "PMReportFormServer",
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
                        name: "FK_PMServerSCADADataBackupDetails_YesNoStatuses_YesNoStatusID",
                        column: x => x.YesNoStatusID,
                        principalTable: "YesNoStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

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
                });

            migrationBuilder.CreateTable(
                name: "PMServerSoftwarePatchSummaries",
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
                    table.PrimaryKey("PK_PMServerSoftwarePatchSummaries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PMServerSoftwarePatchSummaries_PMReportFormServer_PMReportFormServerID",
                        column: x => x.PMReportFormServerID,
                        principalTable: "PMReportFormServer",
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
                        name: "FK_PMServerTimeSyncDetails_ResultStatuses_ResultStatusID",
                        column: x => x.ResultStatusID,
                        principalTable: "ResultStatuses",
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
                });

            migrationBuilder.CreateTable(
                name: "PMServerWillowlynxCCTVCameras",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PMReportFormServerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        name: "FK_PMServerWillowlynxRTUStatuses_YesNoStatuses_YesNoStatusID",
                        column: x => x.YesNoStatusID,
                        principalTable: "YesNoStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportFormImages",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportFormID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportImageTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StoredDirectory = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UploadedStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportFormImages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ReportFormImageTypes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageTypeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportFormImageTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ReportForms",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportFormTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SystemNameWarehouseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StationNameWarehouseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UploadStatus = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UploadHostname = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UploadIPAddress = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    FormStatus = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportForms", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReportForms_StationNameWarehouses_StationNameWarehouseID",
                        column: x => x.StationNameWarehouseID,
                        principalTable: "StationNameWarehouses",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ReportForms_SystemNameWarehouses_SystemNameWarehouseID",
                        column: x => x.SystemNameWarehouseID,
                        principalTable: "SystemNameWarehouses",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ReportFormTypes",
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
                    table.PrimaryKey("PK_ReportFormTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RoomBookings",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApprovedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectionReason = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CancellationReason = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CancelledBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RecurrenceRule = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomBookings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RoomBookings_RoomBookingStatus_StatusID",
                        column: x => x.StatusID,
                        principalTable: "RoomBookingStatus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuildingID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Rooms_Buildings_BuildingID",
                        column: x => x.BuildingID,
                        principalTable: "Buildings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubDepartments",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubDepartments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubDepartments_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubDepartmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    EmergencyContactName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EmergencyContactNumber = table.Column<int>(type: "int", nullable: true),
                    EmergencyRelationship = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DepartmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OccupationLevelID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Users_OccupationLevels_OccupationLevelID",
                        column: x => x.OccupationLevelID,
                        principalTable: "OccupationLevels",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Users_Occupations_OccupationID",
                        column: x => x.OccupationID,
                        principalTable: "Occupations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_SubDepartments_SubDepartmentID",
                        column: x => x.SubDepartmentID,
                        principalTable: "SubDepartments",
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
                name: "UserApplicationAccesses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_UserApplicationAccesses_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserImages",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StoredDirectory = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UploadedStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserImages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserImages_ImageTypes_ImageTypeID",
                        column: x => x.ImageTypeID,
                        principalTable: "ImageTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserImages_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserImages_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserImages_Users_UploadedBy",
                        column: x => x.UploadedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserImages_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessLevels_CreatedBy",
                table: "AccessLevels",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AccessLevels_LevelName",
                table: "AccessLevels",
                column: "LevelName",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_AccessLevels_UpdatedBy",
                table: "AccessLevels",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ApplicationName",
                table: "Applications",
                column: "ApplicationName",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_CreatedBy",
                table: "Applications",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_UpdatedBy",
                table: "Applications",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_CreatedBy",
                table: "Buildings",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_Name",
                table: "Buildings",
                column: "Name",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_UpdatedBy",
                table: "Buildings",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CMMaterialUsed_CMReportFormID",
                table: "CMMaterialUsed",
                column: "CMReportFormID");

            migrationBuilder.CreateIndex(
                name: "IX_CMMaterialUsed_CreatedBy",
                table: "CMMaterialUsed",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CMMaterialUsed_UpdatedBy",
                table: "CMMaterialUsed",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CMReportForms_CMReportFormTypeID",
                table: "CMReportForms",
                column: "CMReportFormTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CMReportForms_CreatedBy",
                table: "CMReportForms",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CMReportForms_FormstatusID",
                table: "CMReportForms",
                column: "FormstatusID");

            migrationBuilder.CreateIndex(
                name: "IX_CMReportForms_FurtherActionTakenID",
                table: "CMReportForms",
                column: "FurtherActionTakenID");

            migrationBuilder.CreateIndex(
                name: "IX_CMReportForms_ReportFormID",
                table: "CMReportForms",
                column: "ReportFormID");

            migrationBuilder.CreateIndex(
                name: "IX_CMReportForms_UpdatedBy",
                table: "CMReportForms",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CMReportFormTypes_CreatedBy",
                table: "CMReportFormTypes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CMReportFormTypes_UpdatedBy",
                table: "CMReportFormTypes",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Company_CreatedBy",
                table: "Company",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Name",
                table: "Company",
                column: "Name",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Company_UpdatedBy",
                table: "Company",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CreatedBy",
                table: "Departments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Name",
                table: "Departments",
                column: "Name",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_UpdatedBy",
                table: "Departments",
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
                name: "IX_FurtherActionTakenWarehouses_CreatedBy",
                table: "FurtherActionTakenWarehouses",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_FurtherActionTakenWarehouses_UpdatedBy",
                table: "FurtherActionTakenWarehouses",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ImageTypes_CreatedBy",
                table: "ImageTypes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ImageTypes_ImageTypeName",
                table: "ImageTypes",
                column: "ImageTypeName",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_ImageTypes_UpdatedBy",
                table: "ImageTypes",
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
                name: "IX_News_CreatedBy",
                table: "News",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_News_NewsCategoryID",
                table: "News",
                column: "NewsCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_News_Slug",
                table: "News",
                column: "Slug",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_News_UpdatedBy",
                table: "News",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_NewsCategory_CreatedBy",
                table: "NewsCategory",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_NewsCategory_Name",
                table: "NewsCategory",
                column: "Name",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_NewsCategory_ParentCategoryID",
                table: "NewsCategory",
                column: "ParentCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_NewsCategory_Slug",
                table: "NewsCategory",
                column: "Slug",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_NewsCategory_UpdatedBy",
                table: "NewsCategory",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_NewsComments_CreatedBy",
                table: "NewsComments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_NewsComments_NewsID",
                table: "NewsComments",
                column: "NewsID");

            migrationBuilder.CreateIndex(
                name: "IX_NewsComments_ParentCommentID",
                table: "NewsComments",
                column: "ParentCommentID");

            migrationBuilder.CreateIndex(
                name: "IX_NewsComments_UpdatedBy",
                table: "NewsComments",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_NewsComments_UserID",
                table: "NewsComments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_NewsImages_NewsID",
                table: "NewsImages",
                column: "NewsID");

            migrationBuilder.CreateIndex(
                name: "IX_NewsImages_UploadedBy",
                table: "NewsImages",
                column: "UploadedBy");

            migrationBuilder.CreateIndex(
                name: "IX_NewsReactions_CreatedBy",
                table: "NewsReactions",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_NewsReactions_NewsID_UserID",
                table: "NewsReactions",
                columns: new[] { "NewsID", "UserID" },
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_NewsReactions_UserID",
                table: "NewsReactions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_OccupationLevels_CreatedBy",
                table: "OccupationLevels",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_OccupationLevels_LevelName",
                table: "OccupationLevels",
                column: "LevelName",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_OccupationLevels_UpdatedBy",
                table: "OccupationLevels",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Occupations_CreatedBy",
                table: "Occupations",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Occupations_OccupationLevelID",
                table: "Occupations",
                column: "OccupationLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_Occupations_OccupationName_OccupationLevelID",
                table: "Occupations",
                columns: new[] { "OccupationName", "OccupationLevelID" },
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Occupations_UpdatedBy",
                table: "Occupations",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMChamberMagneticContacts_CreatedBy",
                table: "PMChamberMagneticContacts",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMChamberMagneticContacts_PMReportFormRTUID",
                table: "PMChamberMagneticContacts",
                column: "PMReportFormRTUID");

            migrationBuilder.CreateIndex(
                name: "IX_PMChamberMagneticContacts_UpdatedBy",
                table: "PMChamberMagneticContacts",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMDVREquipments_CreatedBy",
                table: "PMDVREquipments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMDVREquipments_PMReportFormRTUID",
                table: "PMDVREquipments",
                column: "PMReportFormRTUID");

            migrationBuilder.CreateIndex(
                name: "IX_PMDVREquipments_UpdatedBy",
                table: "PMDVREquipments",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMMainRtuCabinets_CreatedBy",
                table: "PMMainRtuCabinets",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMMainRtuCabinets_PMReportFormRTUID",
                table: "PMMainRtuCabinets",
                column: "PMReportFormRTUID");

            migrationBuilder.CreateIndex(
                name: "IX_PMMainRtuCabinets_UpdatedBy",
                table: "PMMainRtuCabinets",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMReportFormRTU_CreatedBy",
                table: "PMReportFormRTU",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMReportFormRTU_FormstatusID",
                table: "PMReportFormRTU",
                column: "FormstatusID");

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

            migrationBuilder.CreateIndex(
                name: "IX_PMReportFormServer_CreatedBy",
                table: "PMReportFormServer",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PMReportFormServer_FormstatusID",
                table: "PMReportFormServer",
                column: "FormstatusID");

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
                name: "IX_PMRTUCabinetCoolings_PMReportFormRTUID",
                table: "PMRTUCabinetCoolings",
                column: "PMReportFormRTUID");

            migrationBuilder.CreateIndex(
                name: "IX_PMRTUCabinetCoolings_UpdatedBy",
                table: "PMRTUCabinetCoolings",
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
                name: "IX_PMServerNetworkHealths_CreatedBy",
                table: "PMServerNetworkHealths",
                column: "CreatedBy");

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
                name: "IX_PMServerReportFormPDFRequestLogs_PMReportFormServerID",
                table: "PMServerReportFormPDFRequestLogs",
                column: "PMReportFormServerID");

            migrationBuilder.CreateIndex(
                name: "IX_PMServerReportFormPDFRequestLogs_RequestedBy",
                table: "PMServerReportFormPDFRequestLogs",
                column: "RequestedBy");

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
                name: "IX_PMServerWillowlynxRTUStatuses_YesNoStatusID",
                table: "PMServerWillowlynxRTUStatuses",
                column: "YesNoStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFormImages_ReportFormID",
                table: "ReportFormImages",
                column: "ReportFormID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFormImages_ReportImageTypeID",
                table: "ReportFormImages",
                column: "ReportImageTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFormImages_UploadedBy",
                table: "ReportFormImages",
                column: "UploadedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFormImageTypes_CreatedBy",
                table: "ReportFormImageTypes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFormImageTypes_UpdatedBy",
                table: "ReportFormImageTypes",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportForms_CreatedBy",
                table: "ReportForms",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportForms_ReportFormTypeID",
                table: "ReportForms",
                column: "ReportFormTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportForms_StationNameWarehouseID",
                table: "ReportForms",
                column: "StationNameWarehouseID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportForms_SystemNameWarehouseID",
                table: "ReportForms",
                column: "SystemNameWarehouseID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportForms_UpdatedBy",
                table: "ReportForms",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFormTypes_CreatedBy",
                table: "ReportFormTypes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFormTypes_UpdatedBy",
                table: "ReportFormTypes",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RoomBookings_ApprovedBy",
                table: "RoomBookings",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RoomBookings_CancelledBy",
                table: "RoomBookings",
                column: "CancelledBy");

            migrationBuilder.CreateIndex(
                name: "IX_RoomBookings_CreatedBy",
                table: "RoomBookings",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RoomBookings_RequestedBy",
                table: "RoomBookings",
                column: "RequestedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RoomBookings_RoomID",
                table: "RoomBookings",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_RoomBookings_StatusID",
                table: "RoomBookings",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_RoomBookings_UpdatedBy",
                table: "RoomBookings",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_BuildingID",
                table: "Rooms",
                column: "BuildingID");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_CreatedBy",
                table: "Rooms",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_Name_BuildingID",
                table: "Rooms",
                columns: new[] { "Name", "BuildingID" },
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_UpdatedBy",
                table: "Rooms",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_StationNameWarehouses_SystemNameWarehouseID",
                table: "StationNameWarehouses",
                column: "SystemNameWarehouseID");

            migrationBuilder.CreateIndex(
                name: "IX_SubDepartments_CreatedBy",
                table: "SubDepartments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SubDepartments_DepartmentID",
                table: "SubDepartments",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_SubDepartments_UpdatedBy",
                table: "SubDepartments",
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
                name: "IX_UserApplicationAccesses_GrantedBy",
                table: "UserApplicationAccesses",
                column: "GrantedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserApplicationAccesses_UpdatedBy",
                table: "UserApplicationAccesses",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserApplicationAccesses_UserID_ApplicationID",
                table: "UserApplicationAccesses",
                columns: new[] { "UserID", "ApplicationID" },
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_CreatedBy",
                table: "UserImages",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_ImageTypeID",
                table: "UserImages",
                column: "ImageTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_UpdatedBy",
                table: "UserImages",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_UploadedBy",
                table: "UserImages",
                column: "UploadedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_UserID",
                table: "UserImages",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyID",
                table: "Users",
                column: "CompanyID");

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
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Users_OccupationID",
                table: "Users",
                column: "OccupationID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_OccupationLevelID",
                table: "Users",
                column: "OccupationLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StaffCardID",
                table: "Users",
                column: "StaffCardID",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SubDepartmentID",
                table: "Users",
                column: "SubDepartmentID");

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
                name: "FK_Buildings_Users_CreatedBy",
                table: "Buildings",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_Users_UpdatedBy",
                table: "Buildings",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CMMaterialUsed_CMReportForms_CMReportFormID",
                table: "CMMaterialUsed",
                column: "CMReportFormID",
                principalTable: "CMReportForms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CMMaterialUsed_Users_CreatedBy",
                table: "CMMaterialUsed",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CMMaterialUsed_Users_UpdatedBy",
                table: "CMMaterialUsed",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CMReportForms_CMReportFormTypes_CMReportFormTypeID",
                table: "CMReportForms",
                column: "CMReportFormTypeID",
                principalTable: "CMReportFormTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CMReportForms_FormStatusWarehouses_FormstatusID",
                table: "CMReportForms",
                column: "FormstatusID",
                principalTable: "FormStatusWarehouses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CMReportForms_FurtherActionTakenWarehouses_FurtherActionTakenID",
                table: "CMReportForms",
                column: "FurtherActionTakenID",
                principalTable: "FurtherActionTakenWarehouses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CMReportForms_ReportForms_ReportFormID",
                table: "CMReportForms",
                column: "ReportFormID",
                principalTable: "ReportForms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CMReportForms_Users_CreatedBy",
                table: "CMReportForms",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CMReportForms_Users_UpdatedBy",
                table: "CMReportForms",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CMReportFormTypes_Users_CreatedBy",
                table: "CMReportFormTypes",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CMReportFormTypes_Users_UpdatedBy",
                table: "CMReportFormTypes",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Users_CreatedBy",
                table: "Company",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Users_UpdatedBy",
                table: "Company",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Users_CreatedBy",
                table: "Departments",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Users_UpdatedBy",
                table: "Departments",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FormStatusWarehouses_Users_CreatedBy",
                table: "FormStatusWarehouses",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FormStatusWarehouses_Users_UpdatedBy",
                table: "FormStatusWarehouses",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FurtherActionTakenWarehouses_Users_CreatedBy",
                table: "FurtherActionTakenWarehouses",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FurtherActionTakenWarehouses_Users_UpdatedBy",
                table: "FurtherActionTakenWarehouses",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageTypes_Users_CreatedBy",
                table: "ImageTypes",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ImageTypes_Users_UpdatedBy",
                table: "ImageTypes",
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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImportFileRecords_Users_CreatedBy",
                table: "ImportFileRecords",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ImportFileRecords_Users_UpdatedBy",
                table: "ImportFileRecords",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ImportFileRecords_Users_UploadedBy",
                table: "ImportFileRecords",
                column: "UploadedBy",
                principalTable: "Users",
                principalColumn: "ID");

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
                name: "FK_News_NewsCategory_NewsCategoryID",
                table: "News",
                column: "NewsCategoryID",
                principalTable: "NewsCategory",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_News_Users_CreatedBy",
                table: "News",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_News_Users_UpdatedBy",
                table: "News",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsCategory_Users_CreatedBy",
                table: "NewsCategory",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsCategory_Users_UpdatedBy",
                table: "NewsCategory",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsComments_Users_CreatedBy",
                table: "NewsComments",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsComments_Users_UpdatedBy",
                table: "NewsComments",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsComments_Users_UserID",
                table: "NewsComments",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsImages_Users_UploadedBy",
                table: "NewsImages",
                column: "UploadedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsReactions_Users_CreatedBy",
                table: "NewsReactions",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsReactions_Users_UserID",
                table: "NewsReactions",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OccupationLevels_Users_CreatedBy",
                table: "OccupationLevels",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OccupationLevels_Users_UpdatedBy",
                table: "OccupationLevels",
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

            migrationBuilder.AddForeignKey(
                name: "FK_PMChamberMagneticContacts_PMReportFormRTU_PMReportFormRTUID",
                table: "PMChamberMagneticContacts",
                column: "PMReportFormRTUID",
                principalTable: "PMReportFormRTU",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMChamberMagneticContacts_Users_CreatedBy",
                table: "PMChamberMagneticContacts",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMChamberMagneticContacts_Users_UpdatedBy",
                table: "PMChamberMagneticContacts",
                column: "UpdatedBy",
                principalTable: "Users",
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
                name: "FK_PMDVREquipments_Users_CreatedBy",
                table: "PMDVREquipments",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMDVREquipments_Users_UpdatedBy",
                table: "PMDVREquipments",
                column: "UpdatedBy",
                principalTable: "Users",
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
                name: "FK_PMMainRtuCabinets_Users_CreatedBy",
                table: "PMMainRtuCabinets",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMMainRtuCabinets_Users_UpdatedBy",
                table: "PMMainRtuCabinets",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMReportFormRTU_PMReportFormTypes_PMReportFormTypeID",
                table: "PMReportFormRTU",
                column: "PMReportFormTypeID",
                principalTable: "PMReportFormTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMReportFormRTU_ReportForms_ReportFormID",
                table: "PMReportFormRTU",
                column: "ReportFormID",
                principalTable: "ReportForms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMReportFormRTU_Users_CreatedBy",
                table: "PMReportFormRTU",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMReportFormRTU_Users_UpdatedBy",
                table: "PMReportFormRTU",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMReportFormServer_PMReportFormTypes_PMReportFormTypeID",
                table: "PMReportFormServer",
                column: "PMReportFormTypeID",
                principalTable: "PMReportFormTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMReportFormServer_ReportForms_ReportFormID",
                table: "PMReportFormServer",
                column: "ReportFormID",
                principalTable: "ReportForms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMReportFormServer_Users_CreatedBy",
                table: "PMReportFormServer",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMReportFormServer_Users_UpdatedBy",
                table: "PMReportFormServer",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMReportFormTypes_Users_CreatedBy",
                table: "PMReportFormTypes",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PMReportFormTypes_Users_UpdatedBy",
                table: "PMReportFormTypes",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PMRTUCabinetCoolings_Users_CreatedBy",
                table: "PMRTUCabinetCoolings",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMRTUCabinetCoolings_Users_UpdatedBy",
                table: "PMRTUCabinetCoolings",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerASAFirewalls_Users_CreatedBy",
                table: "PMServerASAFirewalls",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerASAFirewalls_Users_UpdatedBy",
                table: "PMServerASAFirewalls",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerCPUAndMemoryUsages_Users_CreatedBy",
                table: "PMServerCPUAndMemoryUsages",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerCPUAndMemoryUsages_Users_UpdatedBy",
                table: "PMServerCPUAndMemoryUsages",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerCPUUsageDetails_Users_CreatedBy",
                table: "PMServerCPUUsageDetails",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerCPUUsageDetails_Users_UpdatedBy",
                table: "PMServerCPUUsageDetails",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerDatabaseBackups_Users_CreatedBy",
                table: "PMServerDatabaseBackups",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerDatabaseBackups_Users_UpdatedBy",
                table: "PMServerDatabaseBackups",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerDiskUsageHealthDetails_PMServerDiskUsageHealths_PMServerDiskUsageHealthID",
                table: "PMServerDiskUsageHealthDetails",
                column: "PMServerDiskUsageHealthID",
                principalTable: "PMServerDiskUsageHealths",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerDiskUsageHealthDetails_Users_CreatedBy",
                table: "PMServerDiskUsageHealthDetails",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerDiskUsageHealthDetails_Users_UpdatedBy",
                table: "PMServerDiskUsageHealthDetails",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerDiskUsageHealths_Users_CreatedBy",
                table: "PMServerDiskUsageHealths",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerDiskUsageHealths_Users_UpdatedBy",
                table: "PMServerDiskUsageHealths",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerFailOverDetails_PMServerFailOvers_PMServerFailOverID",
                table: "PMServerFailOverDetails",
                column: "PMServerFailOverID",
                principalTable: "PMServerFailOvers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerFailOverDetails_Users_CreatedBy",
                table: "PMServerFailOverDetails",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerFailOverDetails_Users_UpdatedBy",
                table: "PMServerFailOverDetails",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerFailOvers_Users_CreatedBy",
                table: "PMServerFailOvers",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerFailOvers_Users_UpdatedBy",
                table: "PMServerFailOvers",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerHardDriveHealthDetails_PMServerHardDriveHealths_PMServerHardDriveHealthID",
                table: "PMServerHardDriveHealthDetails",
                column: "PMServerHardDriveHealthID",
                principalTable: "PMServerHardDriveHealths",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerHardDriveHealthDetails_Users_CreatedBy",
                table: "PMServerHardDriveHealthDetails",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerHardDriveHealthDetails_Users_UpdatedBy",
                table: "PMServerHardDriveHealthDetails",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerHardDriveHealths_Users_CreatedBy",
                table: "PMServerHardDriveHealths",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerHardDriveHealths_Users_UpdatedBy",
                table: "PMServerHardDriveHealths",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerHealthDetails_PMServerHealths_PMServerHealthID",
                table: "PMServerHealthDetails",
                column: "PMServerHealthID",
                principalTable: "PMServerHealths",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerHealthDetails_Users_CreatedBy",
                table: "PMServerHealthDetails",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerHealthDetails_Users_UpdatedBy",
                table: "PMServerHealthDetails",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerHealths_Users_CreatedBy",
                table: "PMServerHealths",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerHealths_Users_UpdatedBy",
                table: "PMServerHealths",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerHotFixes_Users_CreatedBy",
                table: "PMServerHotFixes",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerHotFixes_Users_UpdatedBy",
                table: "PMServerHotFixes",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerHotFixesDetails_Users_CreatedBy",
                table: "PMServerHotFixesDetails",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerHotFixesDetails_Users_UpdatedBy",
                table: "PMServerHotFixesDetails",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerMemoryUsageDetails_Users_CreatedBy",
                table: "PMServerMemoryUsageDetails",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerMemoryUsageDetails_Users_UpdatedBy",
                table: "PMServerMemoryUsageDetails",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerMonthlyDatabaseCreationDetails_PMServerMonthlyDatabaseCreations_PMServerMonthlyDatabaseCreationID",
                table: "PMServerMonthlyDatabaseCreationDetails",
                column: "PMServerMonthlyDatabaseCreationID",
                principalTable: "PMServerMonthlyDatabaseCreations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerMonthlyDatabaseCreationDetails_Users_CreatedBy",
                table: "PMServerMonthlyDatabaseCreationDetails",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerMonthlyDatabaseCreationDetails_Users_UpdatedBy",
                table: "PMServerMonthlyDatabaseCreationDetails",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerMonthlyDatabaseCreations_Users_CreatedBy",
                table: "PMServerMonthlyDatabaseCreations",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerMonthlyDatabaseCreations_Users_UpdatedBy",
                table: "PMServerMonthlyDatabaseCreations",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerMSSQLDatabaseBackupDetails_Users_CreatedBy",
                table: "PMServerMSSQLDatabaseBackupDetails",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerMSSQLDatabaseBackupDetails_Users_UpdatedBy",
                table: "PMServerMSSQLDatabaseBackupDetails",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerNetworkHealths_Users_CreatedBy",
                table: "PMServerNetworkHealths",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerNetworkHealths_Users_UpdatedBy",
                table: "PMServerNetworkHealths",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerReportFormPDFRequestLogs_Users_RequestedBy",
                table: "PMServerReportFormPDFRequestLogs",
                column: "RequestedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerSCADADataBackupDetails_Users_CreatedBy",
                table: "PMServerSCADADataBackupDetails",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerSCADADataBackupDetails_Users_UpdatedBy",
                table: "PMServerSCADADataBackupDetails",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerSoftwarePatchDetails_PMServerSoftwarePatchSummaries_PMServerSoftwarePatchSummaryID",
                table: "PMServerSoftwarePatchDetails",
                column: "PMServerSoftwarePatchSummaryID",
                principalTable: "PMServerSoftwarePatchSummaries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerSoftwarePatchDetails_Users_CreatedBy",
                table: "PMServerSoftwarePatchDetails",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerSoftwarePatchDetails_Users_UpdatedBy",
                table: "PMServerSoftwarePatchDetails",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerSoftwarePatchSummaries_Users_CreatedBy",
                table: "PMServerSoftwarePatchSummaries",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerSoftwarePatchSummaries_Users_UpdatedBy",
                table: "PMServerSoftwarePatchSummaries",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerTimeSyncDetails_PMServerTimeSyncs_PMServerTimeSyncID",
                table: "PMServerTimeSyncDetails",
                column: "PMServerTimeSyncID",
                principalTable: "PMServerTimeSyncs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerTimeSyncDetails_Users_CreatedBy",
                table: "PMServerTimeSyncDetails",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerTimeSyncDetails_Users_UpdatedBy",
                table: "PMServerTimeSyncDetails",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerTimeSyncs_Users_CreatedBy",
                table: "PMServerTimeSyncs",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerTimeSyncs_Users_UpdatedBy",
                table: "PMServerTimeSyncs",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerWillowlynxCCTVCameras_Users_CreatedBy",
                table: "PMServerWillowlynxCCTVCameras",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerWillowlynxCCTVCameras_Users_UpdatedBy",
                table: "PMServerWillowlynxCCTVCameras",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerWillowlynxHistoricalReports_Users_CreatedBy",
                table: "PMServerWillowlynxHistoricalReports",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerWillowlynxHistoricalReports_Users_UpdatedBy",
                table: "PMServerWillowlynxHistoricalReports",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerWillowlynxHistoricalTrends_Users_CreatedBy",
                table: "PMServerWillowlynxHistoricalTrends",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerWillowlynxHistoricalTrends_Users_UpdatedBy",
                table: "PMServerWillowlynxHistoricalTrends",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerWillowlynxNetworkStatuses_Users_CreatedBy",
                table: "PMServerWillowlynxNetworkStatuses",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerWillowlynxNetworkStatuses_Users_UpdatedBy",
                table: "PMServerWillowlynxNetworkStatuses",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerWillowlynxProcessStatuses_Users_CreatedBy",
                table: "PMServerWillowlynxProcessStatuses",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerWillowlynxProcessStatuses_Users_UpdatedBy",
                table: "PMServerWillowlynxProcessStatuses",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerWillowlynxRTUStatuses_Users_CreatedBy",
                table: "PMServerWillowlynxRTUStatuses",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerWillowlynxRTUStatuses_Users_UpdatedBy",
                table: "PMServerWillowlynxRTUStatuses",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportFormImages_ReportFormImageTypes_ReportImageTypeID",
                table: "ReportFormImages",
                column: "ReportImageTypeID",
                principalTable: "ReportFormImageTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportFormImages_ReportForms_ReportFormID",
                table: "ReportFormImages",
                column: "ReportFormID",
                principalTable: "ReportForms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportFormImages_Users_UploadedBy",
                table: "ReportFormImages",
                column: "UploadedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportFormImageTypes_Users_CreatedBy",
                table: "ReportFormImageTypes",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportFormImageTypes_Users_UpdatedBy",
                table: "ReportFormImageTypes",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportForms_ReportFormTypes_ReportFormTypeID",
                table: "ReportForms",
                column: "ReportFormTypeID",
                principalTable: "ReportFormTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportForms_Users_CreatedBy",
                table: "ReportForms",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportForms_Users_UpdatedBy",
                table: "ReportForms",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportFormTypes_Users_CreatedBy",
                table: "ReportFormTypes",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportFormTypes_Users_UpdatedBy",
                table: "ReportFormTypes",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomBookings_Rooms_RoomID",
                table: "RoomBookings",
                column: "RoomID",
                principalTable: "Rooms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomBookings_Users_ApprovedBy",
                table: "RoomBookings",
                column: "ApprovedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomBookings_Users_CancelledBy",
                table: "RoomBookings",
                column: "CancelledBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomBookings_Users_CreatedBy",
                table: "RoomBookings",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomBookings_Users_RequestedBy",
                table: "RoomBookings",
                column: "RequestedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomBookings_Users_UpdatedBy",
                table: "RoomBookings",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Users_CreatedBy",
                table: "Rooms",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Users_UpdatedBy",
                table: "Rooms",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubDepartments_Users_CreatedBy",
                table: "SubDepartments",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubDepartments_Users_UpdatedBy",
                table: "SubDepartments",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_Users_CreatedBy",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Company_Users_UpdatedBy",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Users_CreatedBy",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Users_UpdatedBy",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_OccupationLevels_Users_CreatedBy",
                table: "OccupationLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_OccupationLevels_Users_UpdatedBy",
                table: "OccupationLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_Occupations_Users_CreatedBy",
                table: "Occupations");

            migrationBuilder.DropForeignKey(
                name: "FK_Occupations_Users_UpdatedBy",
                table: "Occupations");

            migrationBuilder.DropForeignKey(
                name: "FK_SubDepartments_Users_CreatedBy",
                table: "SubDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_SubDepartments_Users_UpdatedBy",
                table: "SubDepartments");

            migrationBuilder.DropTable(
                name: "CMMaterialUsed");

            migrationBuilder.DropTable(
                name: "ImportFileRecords");

            migrationBuilder.DropTable(
                name: "NewsComments");

            migrationBuilder.DropTable(
                name: "NewsImages");

            migrationBuilder.DropTable(
                name: "NewsReactions");

            migrationBuilder.DropTable(
                name: "PMChamberMagneticContacts");

            migrationBuilder.DropTable(
                name: "PMDVREquipments");

            migrationBuilder.DropTable(
                name: "PMMainRtuCabinets");

            migrationBuilder.DropTable(
                name: "PMRTUCabinetCoolings");

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
                name: "PMServerMonthlyDatabaseCreationDetails");

            migrationBuilder.DropTable(
                name: "PMServerMSSQLDatabaseBackupDetails");

            migrationBuilder.DropTable(
                name: "PMServerNetworkHealths");

            migrationBuilder.DropTable(
                name: "PMServerReportFormPDFRequestLogs");

            migrationBuilder.DropTable(
                name: "PMServerSCADADataBackupDetails");

            migrationBuilder.DropTable(
                name: "PMServerSoftwarePatchDetails");

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
                name: "ReportFormImages");

            migrationBuilder.DropTable(
                name: "RoomBookings");

            migrationBuilder.DropTable(
                name: "UserApplicationAccesses");

            migrationBuilder.DropTable(
                name: "UserImages");

            migrationBuilder.DropTable(
                name: "CMReportForms");

            migrationBuilder.DropTable(
                name: "ImportFormTypes");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "PMReportFormRTU");

            migrationBuilder.DropTable(
                name: "ASAFirewallStatuses");

            migrationBuilder.DropTable(
                name: "PMServerDiskUsageHealths");

            migrationBuilder.DropTable(
                name: "ServerDiskStatuses");

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
                name: "PMServerDatabaseBackups");

            migrationBuilder.DropTable(
                name: "PMServerSoftwarePatchSummaries");

            migrationBuilder.DropTable(
                name: "PMServerTimeSyncs");

            migrationBuilder.DropTable(
                name: "ResultStatuses");

            migrationBuilder.DropTable(
                name: "YesNoStatuses");

            migrationBuilder.DropTable(
                name: "ReportFormImageTypes");

            migrationBuilder.DropTable(
                name: "RoomBookingStatus");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "AccessLevels");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "ImageTypes");

            migrationBuilder.DropTable(
                name: "CMReportFormTypes");

            migrationBuilder.DropTable(
                name: "FurtherActionTakenWarehouses");

            migrationBuilder.DropTable(
                name: "NewsCategory");

            migrationBuilder.DropTable(
                name: "PMReportFormServer");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "FormStatusWarehouses");

            migrationBuilder.DropTable(
                name: "PMReportFormTypes");

            migrationBuilder.DropTable(
                name: "ReportForms");

            migrationBuilder.DropTable(
                name: "ReportFormTypes");

            migrationBuilder.DropTable(
                name: "StationNameWarehouses");

            migrationBuilder.DropTable(
                name: "SystemNameWarehouses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Occupations");

            migrationBuilder.DropTable(
                name: "SubDepartments");

            migrationBuilder.DropTable(
                name: "OccupationLevels");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
