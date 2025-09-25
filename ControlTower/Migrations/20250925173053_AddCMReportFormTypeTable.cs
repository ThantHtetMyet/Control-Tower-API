using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class AddCMReportFormTypeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CMReportFormTypeID",
                table: "CMReportForms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
                    table.ForeignKey(
                        name: "FK_CMReportFormTypes_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_CMReportFormTypes_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CMReportForms_CMReportFormTypeID",
                table: "CMReportForms",
                column: "CMReportFormTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CMReportFormTypes_CreatedBy",
                table: "CMReportFormTypes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CMReportFormTypes_UpdatedBy",
                table: "CMReportFormTypes",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_CMReportForms_CMReportFormTypes_CMReportFormTypeID",
                table: "CMReportForms",
                column: "CMReportFormTypeID",
                principalTable: "CMReportFormTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CMReportForms_CMReportFormTypes_CMReportFormTypeID",
                table: "CMReportForms");

            migrationBuilder.DropTable(
                name: "CMReportFormTypes");

            migrationBuilder.DropIndex(
                name: "IX_CMReportForms_CMReportFormTypeID",
                table: "CMReportForms");

            migrationBuilder.DropColumn(
                name: "CMReportFormTypeID",
                table: "CMReportForms");
        }
    }
}
