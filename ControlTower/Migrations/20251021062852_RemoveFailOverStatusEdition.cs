using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFailOverStatusEdition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PMServerFailOverDetails_PMServerFailOvers_PMServerFailOverID",
                table: "PMServerFailOverDetails");

            migrationBuilder.AlterColumn<Guid>(
                name: "PMServerFailOverID",
                table: "PMServerFailOverDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerFailOverDetails_PMServerFailOvers_PMServerFailOverID",
                table: "PMServerFailOverDetails",
                column: "PMServerFailOverID",
                principalTable: "PMServerFailOvers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PMServerFailOverDetails_PMServerFailOvers_PMServerFailOverID",
                table: "PMServerFailOverDetails");

            migrationBuilder.AlterColumn<Guid>(
                name: "PMServerFailOverID",
                table: "PMServerFailOverDetails",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerFailOverDetails_PMServerFailOvers_PMServerFailOverID",
                table: "PMServerFailOverDetails",
                column: "PMServerFailOverID",
                principalTable: "PMServerFailOvers",
                principalColumn: "ID");
        }
    }
}
