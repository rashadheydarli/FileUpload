using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PurpleBuzz.Migrations
{
    /// <inheritdoc />
    public partial class FeaturedWorkComponentAndPhotoAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeaturedWorkComponent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeaturedWorkComponent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeaturedWorkComponentPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeaturedWorkComponentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeaturedWorkComponentPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeaturedWorkComponentPhotos_FeaturedWorkComponent_FeaturedWorkComponentId",
                        column: x => x.FeaturedWorkComponentId,
                        principalTable: "FeaturedWorkComponent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeaturedWorkComponentPhotos_FeaturedWorkComponentId",
                table: "FeaturedWorkComponentPhotos",
                column: "FeaturedWorkComponentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeaturedWorkComponentPhotos");

            migrationBuilder.DropTable(
                name: "FeaturedWorkComponent");
        }
    }
}
