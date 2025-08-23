using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNameToNewCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_NewsCategories_NewsCategoryID",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsCategories_NewsCategories_ParentCategoryID",
                table: "NewsCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsCategories_Users_CreatedBy",
                table: "NewsCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsCategories_Users_UpdatedBy",
                table: "NewsCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsCategories",
                table: "NewsCategories");

            migrationBuilder.RenameTable(
                name: "NewsCategories",
                newName: "NewsCategory");

            migrationBuilder.RenameIndex(
                name: "IX_NewsCategories_UpdatedBy",
                table: "NewsCategory",
                newName: "IX_NewsCategory_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_NewsCategories_Slug",
                table: "NewsCategory",
                newName: "IX_NewsCategory_Slug");

            migrationBuilder.RenameIndex(
                name: "IX_NewsCategories_ParentCategoryID",
                table: "NewsCategory",
                newName: "IX_NewsCategory_ParentCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_NewsCategories_Name",
                table: "NewsCategory",
                newName: "IX_NewsCategory_Name");

            migrationBuilder.RenameIndex(
                name: "IX_NewsCategories_CreatedBy",
                table: "NewsCategory",
                newName: "IX_NewsCategory_CreatedBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsCategory",
                table: "NewsCategory",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_News_NewsCategory_NewsCategoryID",
                table: "News",
                column: "NewsCategoryID",
                principalTable: "NewsCategory",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsCategory_NewsCategory_ParentCategoryID",
                table: "NewsCategory",
                column: "ParentCategoryID",
                principalTable: "NewsCategory",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsCategory_Users_CreatedBy",
                table: "NewsCategory",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsCategory_Users_UpdatedBy",
                table: "NewsCategory",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_NewsCategory_NewsCategoryID",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsCategory_NewsCategory_ParentCategoryID",
                table: "NewsCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsCategory_Users_CreatedBy",
                table: "NewsCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsCategory_Users_UpdatedBy",
                table: "NewsCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsCategory",
                table: "NewsCategory");

            migrationBuilder.RenameTable(
                name: "NewsCategory",
                newName: "NewsCategories");

            migrationBuilder.RenameIndex(
                name: "IX_NewsCategory_UpdatedBy",
                table: "NewsCategories",
                newName: "IX_NewsCategories_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_NewsCategory_Slug",
                table: "NewsCategories",
                newName: "IX_NewsCategories_Slug");

            migrationBuilder.RenameIndex(
                name: "IX_NewsCategory_ParentCategoryID",
                table: "NewsCategories",
                newName: "IX_NewsCategories_ParentCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_NewsCategory_Name",
                table: "NewsCategories",
                newName: "IX_NewsCategories_Name");

            migrationBuilder.RenameIndex(
                name: "IX_NewsCategory_CreatedBy",
                table: "NewsCategories",
                newName: "IX_NewsCategories_CreatedBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsCategories",
                table: "NewsCategories",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_News_NewsCategories_NewsCategoryID",
                table: "News",
                column: "NewsCategoryID",
                principalTable: "NewsCategories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsCategories_NewsCategories_ParentCategoryID",
                table: "NewsCategories",
                column: "ParentCategoryID",
                principalTable: "NewsCategories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsCategories_Users_CreatedBy",
                table: "NewsCategories",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsCategories_Users_UpdatedBy",
                table: "NewsCategories",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
