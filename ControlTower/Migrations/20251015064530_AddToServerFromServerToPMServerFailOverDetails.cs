using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class AddToServerFromServerToPMServerFailOverDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PMServerFailOverDetails_FailOverStatuses_FailOverStatusID",
                table: "PMServerFailOverDetails");

            migrationBuilder.DropTable(
                name: "FailOverStatuses");

            migrationBuilder.DropIndex(
                name: "IX_PMServerFailOverDetails_FailOverStatusID",
                table: "PMServerFailOverDetails");

            migrationBuilder.DropColumn(
                name: "FailOverStatusID",
                table: "PMServerFailOverDetails");

            migrationBuilder.AddColumn<string>(
                name: "FromServer",
                table: "PMServerFailOverDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToServer",
                table: "PMServerFailOverDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromServer",
                table: "PMServerFailOverDetails");

            migrationBuilder.DropColumn(
                name: "ToServer",
                table: "PMServerFailOverDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "FailOverStatusID",
                table: "PMServerFailOverDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "FailOverStatuses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FailOverStatuses", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PMServerFailOverDetails_FailOverStatusID",
                table: "PMServerFailOverDetails",
                column: "FailOverStatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_PMServerFailOverDetails_FailOverStatuses_FailOverStatusID",
                table: "PMServerFailOverDetails",
                column: "FailOverStatusID",
                principalTable: "FailOverStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
