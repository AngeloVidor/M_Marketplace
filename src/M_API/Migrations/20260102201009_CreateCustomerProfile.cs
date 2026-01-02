using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace M_API.Migrations
{
    /// <inheritdoc />
    public partial class CreateCustomerProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "CustomerProfiles");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "CustomerProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "CustomerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "Address_Complement",
                table: "CustomerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_Neighborhood",
                table: "CustomerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_Number",
                table: "CustomerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "CustomerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "CustomerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "CustomerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "CustomerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "CustomerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "CustomerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "CustomerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_Complement",
                table: "CustomerProfiles");

            migrationBuilder.DropColumn(
                name: "Address_Neighborhood",
                table: "CustomerProfiles");

            migrationBuilder.DropColumn(
                name: "Address_Number",
                table: "CustomerProfiles");

            migrationBuilder.DropColumn(
                name: "City",
                table: "CustomerProfiles");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "CustomerProfiles");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "CustomerProfiles");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "CustomerProfiles");

            migrationBuilder.DropColumn(
                name: "State",
                table: "CustomerProfiles");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "CustomerProfiles");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "CustomerProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "CustomerProfiles",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "CustomerProfiles",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "CustomerProfiles",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }
    }
}
