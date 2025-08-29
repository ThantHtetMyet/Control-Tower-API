using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class FixUniqueConstraintsForSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NewsCategory_Name",
                table: "NewsCategory");

            migrationBuilder.DropIndex(
                name: "IX_NewsCategory_Slug",
                table: "NewsCategory");

            migrationBuilder.CreateIndex(
                name: "IX_NewsCategory_Name",
                table: "NewsCategory",
                column: "Name",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_NewsCategory_Slug",
                table: "NewsCategory",
                column: "Slug",
                unique: true,
                filter: "[IsDeleted] = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NewsCategory_Name",
                table: "NewsCategory");

            migrationBuilder.DropIndex(
                name: "IX_NewsCategory_Slug",
                table: "NewsCategory");

            migrationBuilder.CreateIndex(
                name: "IX_NewsCategory_Name",
                table: "NewsCategory",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NewsCategory_Slug",
                table: "NewsCategory",
                column: "Slug",
                unique: true);
        }
    }
}
