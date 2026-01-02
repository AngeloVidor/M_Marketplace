using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace M_API.Migrations
{
    /// <inheritdoc />
    public partial class RemovingActivationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivationTokens");

            migrationBuilder.AddColumn<string>(
                name: "ActivationCode",
                table: "PendingRegistrations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ActivationToken_Id",
                table: "PendingRegistrations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ActivationToken_UserId",
                table: "PendingRegistrations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiresAt",
                table: "PendingRegistrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivationCode",
                table: "PendingRegistrations");

            migrationBuilder.DropColumn(
                name: "ActivationToken_Id",
                table: "PendingRegistrations");

            migrationBuilder.DropColumn(
                name: "ActivationToken_UserId",
                table: "PendingRegistrations");

            migrationBuilder.DropColumn(
                name: "ExpiresAt",
                table: "PendingRegistrations");

            migrationBuilder.CreateTable(
                name: "ActivationTokens",
                columns: table => new
                {
                    PendingRegistrationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivationTokens", x => x.PendingRegistrationId);
                    table.ForeignKey(
                        name: "FK_ActivationTokens_PendingRegistrations_PendingRegistrationId",
                        column: x => x.PendingRegistrationId,
                        principalTable: "PendingRegistrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
