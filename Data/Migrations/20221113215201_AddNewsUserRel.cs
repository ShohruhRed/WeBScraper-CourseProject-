using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeBScraper_CourseProject_.Data.Migrations
{
    public partial class AddNewsUserRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLikes",
                table: "NewsDb",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "News_Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewsId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_Users_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_News_Users_NewsDb_NewsId",
                        column: x => x.NewsId,
                        principalTable: "NewsDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_News_Users_NewsId",
                table: "News_Users",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_News_Users_UserId",
                table: "News_Users",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "News_Users");

            migrationBuilder.DropColumn(
                name: "IsLikes",
                table: "NewsDb");
        }
    }
}
