using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class AddReportFormFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormStatusWarehouses_Users_CreatedBy",
                table: "FormStatusWarehouses");

            migrationBuilder.DropForeignKey(
                name: "FK_FormStatusWarehouses_Users_UpdatedBy",
                table: "FormStatusWarehouses");

            migrationBuilder.DropForeignKey(
                name: "FK_FurtherActionTakenWarehouses_Users_CreatedBy",
                table: "FurtherActionTakenWarehouses");

            migrationBuilder.DropForeignKey(
                name: "FK_FurtherActionTakenWarehouses_Users_UpdatedBy",
                table: "FurtherActionTakenWarehouses");

            migrationBuilder.DropForeignKey(
                name: "FK_ImportFileRecords_ImportFormTypes_ImportFormTypeID",
                table: "ImportFileRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_ImportFileRecords_Users_CreatedBy",
                table: "ImportFileRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_ImportFileRecords_Users_UpdatedBy",
                table: "ImportFileRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_ImportFileRecords_Users_UploadedBy",
                table: "ImportFileRecords");

            migrationBuilder.DropTable(
                name: "ActionTaken");

            migrationBuilder.DropTable(
                name: "FormStatus");

            migrationBuilder.DropTable(
                name: "FurtherActionTaken");

            migrationBuilder.DropTable(
                name: "IssueFound");

            migrationBuilder.DropTable(
                name: "IssueReported");

            migrationBuilder.DropTable(
                name: "MaterialsUsed");

            migrationBuilder.DropTable(
                name: "ServiceType");

            migrationBuilder.DropTable(
                name: "ServiceReportForms");

            migrationBuilder.DropTable(
                name: "ServiceTypeWarehouses");

            migrationBuilder.DropTable(
                name: "FollowupActionWarehouses");

            migrationBuilder.DropTable(
                name: "LocationWarehouses");

            migrationBuilder.DropTable(
                name: "ProjectNoWarehouses");

            migrationBuilder.DropTable(
                name: "SystemWarehouses");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ImportFormTypes",
                type: "nvarchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "UploadedStatus",
                table: "ImportFileRecords",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UploadedDate",
                table: "ImportFileRecords",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StoredDirectory",
                table: "ImportFileRecords",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImportedStatus",
                table: "ImportFileRecords",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FurtherActionTakenWarehouses",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FormStatusWarehouses",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

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
                    table.ForeignKey(
                        name: "FK_ReportFormImageTypes_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ReportFormImageTypes_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
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
                    table.ForeignKey(
                        name: "FK_ReportFormTypes_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ReportFormTypes_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ReportForms",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportFormTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        name: "FK_ReportForms_ReportFormTypes_ReportFormTypeID",
                        column: x => x.ReportFormTypeID,
                        principalTable: "ReportFormTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportForms_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ReportForms_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "CMReportForms",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportFormID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FurtherActionTakenID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormstatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Customer = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProjectNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SystemDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IssueReportedDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IssueReportedRemark = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IssueFoundDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IssueFoundRemark = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ActionTakenDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ActionTakenRemark = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    FailureDetectedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResponseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DoneBy = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ApprovedBy = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CMReportForms", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CMReportForms_FormStatusWarehouses_FormstatusID",
                        column: x => x.FormstatusID,
                        principalTable: "FormStatusWarehouses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CMReportForms_FurtherActionTakenWarehouses_FurtherActionTakenID",
                        column: x => x.FurtherActionTakenID,
                        principalTable: "FurtherActionTakenWarehouses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CMReportForms_ReportForms_ReportFormID",
                        column: x => x.ReportFormID,
                        principalTable: "ReportForms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CMReportForms_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_CMReportForms_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
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
                    table.ForeignKey(
                        name: "FK_ReportFormImages_ReportFormImageTypes_ReportImageTypeID",
                        column: x => x.ReportImageTypeID,
                        principalTable: "ReportFormImageTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportFormImages_ReportForms_ReportFormID",
                        column: x => x.ReportFormID,
                        principalTable: "ReportForms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportFormImages_Users_UploadedBy",
                        column: x => x.UploadedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "MaterialUsed",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CMReportFormID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SerialNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialUsed", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MaterialUsed_CMReportForms_CMReportFormID",
                        column: x => x.CMReportFormID,
                        principalTable: "CMReportForms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialUsed_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_MaterialUsed_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

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
                name: "IX_MaterialUsed_CMReportFormID",
                table: "MaterialUsed",
                column: "CMReportFormID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialUsed_CreatedBy",
                table: "MaterialUsed",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialUsed_UpdatedBy",
                table: "MaterialUsed",
                column: "UpdatedBy");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormStatusWarehouses_Users_CreatedBy",
                table: "FormStatusWarehouses");

            migrationBuilder.DropForeignKey(
                name: "FK_FormStatusWarehouses_Users_UpdatedBy",
                table: "FormStatusWarehouses");

            migrationBuilder.DropForeignKey(
                name: "FK_FurtherActionTakenWarehouses_Users_CreatedBy",
                table: "FurtherActionTakenWarehouses");

            migrationBuilder.DropForeignKey(
                name: "FK_FurtherActionTakenWarehouses_Users_UpdatedBy",
                table: "FurtherActionTakenWarehouses");

            migrationBuilder.DropForeignKey(
                name: "FK_ImportFileRecords_ImportFormTypes_ImportFormTypeID",
                table: "ImportFileRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_ImportFileRecords_Users_CreatedBy",
                table: "ImportFileRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_ImportFileRecords_Users_UpdatedBy",
                table: "ImportFileRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_ImportFileRecords_Users_UploadedBy",
                table: "ImportFileRecords");

            migrationBuilder.DropTable(
                name: "MaterialUsed");

            migrationBuilder.DropTable(
                name: "ReportFormImages");

            migrationBuilder.DropTable(
                name: "CMReportForms");

            migrationBuilder.DropTable(
                name: "ReportFormImageTypes");

            migrationBuilder.DropTable(
                name: "ReportForms");

            migrationBuilder.DropTable(
                name: "ReportFormTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ImportFormTypes",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "UploadedStatus",
                table: "ImportFileRecords",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UploadedDate",
                table: "ImportFileRecords",
                type: "Date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StoredDirectory",
                table: "ImportFileRecords",
                type: "nvarchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "ImportedStatus",
                table: "ImportFileRecords",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FurtherActionTakenWarehouses",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FormStatusWarehouses",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.CreateTable(
                name: "FollowupActionWarehouses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FollowupActionNo = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowupActionWarehouses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FollowupActionWarehouses_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FollowupActionWarehouses_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocationWarehouses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationWarehouses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LocationWarehouses_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocationWarehouses_Users_UpdatedBy",
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
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ProjectNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "ServiceReportForms",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FollowupActionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LocationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProjectNoID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SystemID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FailureDetectedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    JobNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ResponseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                name: "ActionTaken",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceReportFormID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionTaken", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ActionTaken_ServiceReportForms_ServiceReportFormID",
                        column: x => x.ServiceReportFormID,
                        principalTable: "ServiceReportForms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionTaken_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ActionTaken_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "FormStatus",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FormStatusWarehouseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceReportFormID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormStatus", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FormStatus_FormStatusWarehouses_FormStatusWarehouseID",
                        column: x => x.FormStatusWarehouseID,
                        principalTable: "FormStatusWarehouses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormStatus_ServiceReportForms_ServiceReportFormID",
                        column: x => x.ServiceReportFormID,
                        principalTable: "ServiceReportForms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormStatus_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FormStatus_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "FurtherActionTaken",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FurtherActionTakenWarehouseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceReportFormID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FurtherActionTaken", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FurtherActionTaken_FurtherActionTakenWarehouses_FurtherActionTakenWarehouseID",
                        column: x => x.FurtherActionTakenWarehouseID,
                        principalTable: "FurtherActionTakenWarehouses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FurtherActionTaken_ServiceReportForms_ServiceReportFormID",
                        column: x => x.ServiceReportFormID,
                        principalTable: "ServiceReportForms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FurtherActionTaken_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FurtherActionTaken_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "IssueFound",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceReportFormID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueFound", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IssueFound_ServiceReportForms_ServiceReportFormID",
                        column: x => x.ServiceReportFormID,
                        principalTable: "ServiceReportForms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueFound_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_IssueFound_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "IssueReported",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceReportFormID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueReported", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IssueReported_ServiceReportForms_ServiceReportFormID",
                        column: x => x.ServiceReportFormID,
                        principalTable: "ServiceReportForms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueReported_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_IssueReported_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "MaterialsUsed",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceReportFormID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialsUsed", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MaterialsUsed_ServiceReportForms_ServiceReportFormID",
                        column: x => x.ServiceReportFormID,
                        principalTable: "ServiceReportForms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialsUsed_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_MaterialsUsed_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ServiceType",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceTypeWarehouseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceReportFormID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "IX_MaterialsUsed_CreatedBy",
                table: "MaterialsUsed",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialsUsed_ServiceReportFormID",
                table: "MaterialsUsed",
                column: "ServiceReportFormID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialsUsed_UpdatedBy",
                table: "MaterialsUsed",
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
        }
    }
}
