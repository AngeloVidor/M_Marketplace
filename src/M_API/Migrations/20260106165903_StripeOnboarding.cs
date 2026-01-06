using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace M_API.Migrations
{
    /// <inheritdoc />
    public partial class StripeOnboarding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StripeAccountId",
                table: "VendorProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StripeStatus",
                table: "VendorProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StripeAccountId",
                table: "VendorProfiles");

            migrationBuilder.DropColumn(
                name: "StripeStatus",
                table: "VendorProfiles");
        }
    }
}
