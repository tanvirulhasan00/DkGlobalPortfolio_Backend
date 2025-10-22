using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DkGLobalPortfolio.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class SecondMaplinkInProfileDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SecondMapLink",
                table: "CompanyInfos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecondMapLink",
                table: "CompanyInfos");
        }
    }
}
