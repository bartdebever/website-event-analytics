using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BartdeBever.EventTracking.Migrations
{
    /// <inheritdoc />
    public partial class Added_ApplicationId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "application_id",
                table: "event_logs",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_event_logs_application_id",
                table: "event_logs",
                column: "application_id");

            migrationBuilder.AddForeignKey(
                name: "FK_event_logs_applications_application_id",
                table: "event_logs",
                column: "application_id",
                principalTable: "applications",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_event_logs_applications_application_id",
                table: "event_logs");

            migrationBuilder.DropIndex(
                name: "IX_event_logs_application_id",
                table: "event_logs");

            migrationBuilder.DropColumn(
                name: "application_id",
                table: "event_logs");
        }
    }
}
