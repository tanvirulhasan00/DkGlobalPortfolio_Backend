using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DkGLobalPortfolio.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangePartnerImagesToProductImagesTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartnerImages_Products_ProductId",
                table: "PartnerImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartnerImages",
                table: "PartnerImages");

            migrationBuilder.RenameTable(
                name: "PartnerImages",
                newName: "ProductImages");

            migrationBuilder.RenameIndex(
                name: "IX_PartnerImages_ProductId",
                table: "ProductImages",
                newName: "IX_ProductImages_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages");

            migrationBuilder.RenameTable(
                name: "ProductImages",
                newName: "PartnerImages");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_ProductId",
                table: "PartnerImages",
                newName: "IX_PartnerImages_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartnerImages",
                table: "PartnerImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerImages_Products_ProductId",
                table: "PartnerImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
