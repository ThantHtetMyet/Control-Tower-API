using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class RemoveNetworkStatusTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PMServerNetworkHealths_NetworkStatuses_NetworkStatusID",
                table: "PMServerNetworkHealths");

            migrationBuilder.DropTable(
                name: "NetworkStatuses");

            migrationBuilder.DropIndex(
                name: "IX_PMServerNetworkHealths_NetworkStatusID",
                table: "PMServerNetworkHealths");

            migrationBuilder.DropColumn(
                name: "NetworkStatusID",
                table: "PMServerNetworkHealths");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NetworkStatusID",
                table: "PMServerNetworkHealths",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "NetworkStatuses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetworkStatuses", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PMServerNetworkHealths_NetworkStatusID",
                table: "PMServerNetworkHealths",
                column: "NetworkStatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerNetworkHealths_NetworkStatuses_NetworkStatusID",
                table: "PMServerNetworkHealths",
                column: "NetworkStatusID",
                principalTable: "NetworkStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
