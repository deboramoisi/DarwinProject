using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class DeviceUserRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Devices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_AppUserId",
                table: "Devices",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_AspNetUsers_AppUserId",
                table: "Devices",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_AspNetUsers_AppUserId",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_AppUserId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Devices");
        }
    }
}
