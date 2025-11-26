using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFurtherActionTakenColumnInCMReportFormTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CMReportForms_FurtherActionTakenWarehouses_FurtherActionTakenID",
                table: "CMReportForms");

            migrationBuilder.AlterColumn<Guid>(
                name: "FurtherActionTakenID",
                table: "CMReportForms",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_CMReportForms_FurtherActionTakenWarehouses_FurtherActionTakenID",
                table: "CMReportForms",
                column: "FurtherActionTakenID",
                principalTable: "FurtherActionTakenWarehouses",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CMReportForms_FurtherActionTakenWarehouses_FurtherActionTakenID",
                table: "CMReportForms");

            migrationBuilder.AlterColumn<Guid>(
                name: "FurtherActionTakenID",
                table: "CMReportForms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CMReportForms_FurtherActionTakenWarehouses_FurtherActionTakenID",
                table: "CMReportForms",
                column: "FurtherActionTakenID",
                principalTable: "FurtherActionTakenWarehouses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
