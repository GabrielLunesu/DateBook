using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dating_app_server.Migrations
{
    /// <inheritdoc />
    public partial class AddedUSerQuizResponse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "UserResponse",
                table: "QuizResponses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserResponse",
                table: "QuizResponses");
        }
    }
}
