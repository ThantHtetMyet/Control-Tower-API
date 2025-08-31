using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserApplicationAccessUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserApplicationAccesses_UserID_ApplicationID",
                table: "UserApplicationAccesses");

            migrationBuilder.CreateIndex(
                name: "IX_UserApplicationAccesses_UserID_ApplicationID",
                table: "UserApplicationAccesses",
                columns: new[] { "UserID", "ApplicationID" },
                unique: true,
                filter: "[IsDeleted] = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserApplicationAccesses_UserID_ApplicationID",
                table: "UserApplicationAccesses");

            migrationBuilder.CreateIndex(
                name: "IX_UserApplicationAccesses_UserID_ApplicationID",
                table: "UserApplicationAccesses",
                columns: new[] { "UserID", "ApplicationID" },
                unique: true);
        }
    }
}
