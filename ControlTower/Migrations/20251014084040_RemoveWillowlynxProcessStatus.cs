using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class RemoveWillowlynxProcessStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PMServerWillowlynxProcessStatuses_WillowlynxProcessStatuses_WillowlynxProcessStatusID",
                table: "PMServerWillowlynxProcessStatuses");

            migrationBuilder.DropTable(
                name: "WillowlynxProcessStatuses");

            migrationBuilder.DropIndex(
                name: "IX_PMServerWillowlynxProcessStatuses_WillowlynxProcessStatusID",
                table: "PMServerWillowlynxProcessStatuses");

            migrationBuilder.DropColumn(
                name: "WillowlynxProcessStatusID",
                table: "PMServerWillowlynxProcessStatuses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WillowlynxProcessStatusID",
                table: "PMServerWillowlynxProcessStatuses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "WillowlynxProcessStatuses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WillowlynxProcessStatuses", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PMServerWillowlynxProcessStatuses_WillowlynxProcessStatusID",
                table: "PMServerWillowlynxProcessStatuses",
                column: "WillowlynxProcessStatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerWillowlynxProcessStatuses_WillowlynxProcessStatuses_WillowlynxProcessStatusID",
                table: "PMServerWillowlynxProcessStatuses",
                column: "WillowlynxProcessStatusID",
                principalTable: "WillowlynxProcessStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
