using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class AddCMMaterialUsedModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialUsed");

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
                    table.ForeignKey(
                        name: "FK_CMMaterialUsed_CMReportForms_CMReportFormID",
                        column: x => x.CMReportFormID,
                        principalTable: "CMReportForms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CMMaterialUsed_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_CMMaterialUsed_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CMMaterialUsed");

            migrationBuilder.CreateTable(
                name: "MaterialUsed",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CMReportFormID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SerialNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
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
        }
    }
}
