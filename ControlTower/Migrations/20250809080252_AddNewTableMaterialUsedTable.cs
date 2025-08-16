using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTableMaterialUsedTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaterialsUsed",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceReportFormID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialsUsed");
        }
    }
}
