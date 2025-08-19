using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEmployeeToUserKeyword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserApplicationAccesses_Users_EmployeeID",
                table: "UserApplicationAccesses");

            migrationBuilder.RenameColumn(
                name: "EmployeeID",
                table: "UserApplicationAccesses",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_UserApplicationAccesses_EmployeeID_ApplicationID",
                table: "UserApplicationAccesses",
                newName: "IX_UserApplicationAccesses_UserID_ApplicationID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserApplicationAccesses_Users_UserID",
                table: "UserApplicationAccesses",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserApplicationAccesses_Users_UserID",
                table: "UserApplicationAccesses");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "UserApplicationAccesses",
                newName: "EmployeeID");

            migrationBuilder.RenameIndex(
                name: "IX_UserApplicationAccesses_UserID_ApplicationID",
                table: "UserApplicationAccesses",
                newName: "IX_UserApplicationAccesses_EmployeeID_ApplicationID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserApplicationAccesses_Users_EmployeeID",
                table: "UserApplicationAccesses",
                column: "EmployeeID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
