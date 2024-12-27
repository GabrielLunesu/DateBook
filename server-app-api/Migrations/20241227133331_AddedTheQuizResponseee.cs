using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dating_app_server.Migrations
{
    /// <inheritdoc />
    public partial class AddedTheQuizResponseee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizResponses_AspNetUsers_UserId",
                table: "QuizResponses");

            migrationBuilder.DropIndex(
                name: "IX_QuizResponses_UserId",
                table: "QuizResponses");

            migrationBuilder.CreateIndex(
                name: "IX_QuizResponses_UserId",
                table: "QuizResponses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizResponses_AspNetUsers_UserId",
                table: "QuizResponses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizResponses_AspNetUsers_UserId",
                table: "QuizResponses");

            migrationBuilder.DropIndex(
                name: "IX_QuizResponses_UserId",
                table: "QuizResponses");

            migrationBuilder.CreateIndex(
                name: "IX_QuizResponses_UserId",
                table: "QuizResponses",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizResponses_AspNetUsers_UserId",
                table: "QuizResponses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
