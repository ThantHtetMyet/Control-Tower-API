using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class AddEmergencyContactFieldsTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Emergency_Relationship",
                table: "Users",
                newName: "EmergencyRelationship");

            migrationBuilder.RenameColumn(
                name: "Emergency_Contact_Number",
                table: "Users",
                newName: "EmergencyContactNumber");

            migrationBuilder.RenameColumn(
                name: "Emergency_Contact_Name",
                table: "Users",
                newName: "EmergencyContactName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmergencyRelationship",
                table: "Users",
                newName: "Emergency_Relationship");

            migrationBuilder.RenameColumn(
                name: "EmergencyContactNumber",
                table: "Users",
                newName: "Emergency_Contact_Number");

            migrationBuilder.RenameColumn(
                name: "EmergencyContactName",
                table: "Users",
                newName: "Emergency_Contact_Name");
        }
    }
}
