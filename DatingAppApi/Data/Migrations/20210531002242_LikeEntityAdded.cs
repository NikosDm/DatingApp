using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingAppApi.Data.Migrations
{
    public partial class LikeEntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    SourceUserID = table.Column<int>(type: "int", nullable: false),
                    LikedUserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => new { x.SourceUserID, x.LikedUserID });
                    table.ForeignKey(
                        name: "FK_Likes_Users_LikedUserID",
                        column: x => x.LikedUserID,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Likes_Users_SourceUserID",
                        column: x => x.SourceUserID,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Likes_LikedUserID",
                table: "Likes",
                column: "LikedUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Likes");
        }
    }
}
