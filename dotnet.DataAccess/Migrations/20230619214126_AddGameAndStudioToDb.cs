using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dotnet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddGameAndStudioToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Studios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studios", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "Genre", "Title" },
                values: new object[,]
                {
                    { 1, "A game about a witcher", 2, "The Witcher 3" },
                    { 2, "A game about a dragonborn", 2, "The Elder Scrolls V: Skyrim" },
                    { 3, "A game about world of warcraft", 3, "World of Warcraft" }
                });

            migrationBuilder.InsertData(
                table: "Studios",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Blizzard" },
                    { 2, 2, "Bethesda" },
                    { 3, 3, "CD Projekt Red" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Studios");

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
