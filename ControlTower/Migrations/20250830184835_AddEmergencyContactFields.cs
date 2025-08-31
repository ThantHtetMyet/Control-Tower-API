using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class AddEmergencyContactFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Emergency_Contact_Name",
                table: "Users",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Emergency_Contact_Number",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Emergency_Relationship",
                table: "Users",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Emergency_Contact_Name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Emergency_Contact_Number",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Emergency_Relationship",
                table: "Users");
        }
    }
}
