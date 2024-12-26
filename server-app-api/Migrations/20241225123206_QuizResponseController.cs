using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dating_app_server.Migrations
{
    /// <inheritdoc />
    public partial class QuizResponseController : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_AspNetUsers_UserId",
                table: "Quizzes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quizzes",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_UserId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "AgePreference",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "CompletedAt",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "RelationshipType",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "SocialLevel",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "SportImportance",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Quizzes");

            migrationBuilder.RenameTable(
                name: "Quizzes",
                newName: "Quiz");

            migrationBuilder.RenameColumn(
                name: "WeekendActivity",
                table: "Quiz",
                newName: "Question");

            migrationBuilder.RenameColumn(
                name: "QuizId",
                table: "Quiz",
                newName: "Id");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Quiz",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quiz",
                table: "Quiz",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "QuizResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizResponses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizResponses_Quiz_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quiz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizResponses_QuizId",
                table: "QuizResponses",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizResponses_UserId",
                table: "QuizResponses",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizResponses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quiz",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Quiz");

            migrationBuilder.RenameTable(
                name: "Quiz",
                newName: "Quizzes");

            migrationBuilder.RenameColumn(
                name: "Question",
                table: "Quizzes",
                newName: "WeekendActivity");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Quizzes",
                newName: "QuizId");

            migrationBuilder.AddColumn<string>(
                name: "AgePreference",
                table: "Quizzes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedAt",
                table: "Quizzes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RelationshipType",
                table: "Quizzes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SocialLevel",
                table: "Quizzes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SportImportance",
                table: "Quizzes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Quizzes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quizzes",
                table: "Quizzes",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_UserId",
                table: "Quizzes",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_AspNetUsers_UserId",
                table: "Quizzes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
