using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DkGLobalPortfolio.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeReviewStarIntToDecimalInClientTesti : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ReviewStars",
                table: "ClientTestimonials",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ReviewStars",
                table: "ClientTestimonials",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");
        }
    }
}
