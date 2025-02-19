using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningCenter.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAchievement",
                table: "UserAchievement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LessonCompleted",
                table: "LessonCompleted");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "UserAchievement",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "LessonCompleted",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAchievement",
                table: "UserAchievement",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LessonCompleted",
                table: "LessonCompleted",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievement_UserId",
                table: "UserAchievement",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonCompleted_UserId",
                table: "LessonCompleted",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAchievement",
                table: "UserAchievement");

            migrationBuilder.DropIndex(
                name: "IX_UserAchievement_UserId",
                table: "UserAchievement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LessonCompleted",
                table: "LessonCompleted");

            migrationBuilder.DropIndex(
                name: "IX_LessonCompleted_UserId",
                table: "LessonCompleted");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "UserAchievement",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "LessonCompleted",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAchievement",
                table: "UserAchievement",
                columns: new[] { "UserId", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_LessonCompleted",
                table: "LessonCompleted",
                columns: new[] { "UserId", "Id" });
        }
    }
}
