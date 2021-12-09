using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class AddedLikedCommentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikedPosts_Posts_PostsId",
                table: "LikedPosts");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "LikedPosts");

            migrationBuilder.AlterColumn<int>(
                name: "PostsId",
                table: "LikedPosts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "LikedComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CommentsId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Liked = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikedComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikedComments_Comments_CommentsId",
                        column: x => x.CommentsId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikedComments_CommentsId",
                table: "LikedComments",
                column: "CommentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_LikedPosts_Posts_PostsId",
                table: "LikedPosts",
                column: "PostsId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikedPosts_Posts_PostsId",
                table: "LikedPosts");

            migrationBuilder.DropTable(
                name: "LikedComments");

            migrationBuilder.AlterColumn<int>(
                name: "PostsId",
                table: "LikedPosts",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "LikedPosts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_LikedPosts_Posts_PostsId",
                table: "LikedPosts",
                column: "PostsId",
                principalTable: "Posts",
                principalColumn: "Id");
        }
    }
}
