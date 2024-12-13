using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dating_app_server.Migrations
{
    /// <inheritdoc />
    public partial class AddedProfileQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProfileQuestions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectAnswer = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserAnswer = table.Column<bool>(type: "bit", nullable: true),
                    AnsweredByUserId = table.Column<int>(type: "int", nullable: true),
                    AnsweredAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileQuestions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_ProfileQuestions_Users_AnsweredByUserId",
                        column: x => x.AnsweredByUserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_ProfileQuestions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileQuestions_AnsweredByUserId",
                table: "ProfileQuestions",
                column: "AnsweredByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileQuestions_UserId",
                table: "ProfileQuestions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfileQuestions");
        }
    }
}
