using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddStudioToGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudioId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "StudioId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                column: "StudioId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                column: "StudioId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Games_StudioId",
                table: "Games",
                column: "StudioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Studios_StudioId",
                table: "Games",
                column: "StudioId",
                principalTable: "Studios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Studios_StudioId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_StudioId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "StudioId",
                table: "Games");
        }
    }
}
