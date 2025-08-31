using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlTower.Migrations
{
    /// <inheritdoc />
    public partial class AddUserImageAndImageTypeModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImageTypes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageTypeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageTypes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ImageTypes_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImageTypes_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserImages",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StoredDirectory = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UploadedStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserImages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserImages_ImageTypes_ImageTypeID",
                        column: x => x.ImageTypeID,
                        principalTable: "ImageTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserImages_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserImages_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserImages_Users_UploadedBy",
                        column: x => x.UploadedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserImages_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageTypes_CreatedBy",
                table: "ImageTypes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ImageTypes_ImageTypeName",
                table: "ImageTypes",
                column: "ImageTypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImageTypes_UpdatedBy",
                table: "ImageTypes",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_CreatedBy",
                table: "UserImages",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_ImageTypeID",
                table: "UserImages",
                column: "ImageTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_UpdatedBy",
                table: "UserImages",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_UploadedBy",
                table: "UserImages",
                column: "UploadedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_UserID",
                table: "UserImages",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserImages");

            migrationBuilder.DropTable(
                name: "ImageTypes");
        }
    }
}
