using Microsoft.EntityFrameworkCore.Migrations;

namespace interview.Migrations
{
    public partial class update03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserExam_Exam_ExamId",
                table: "UserExam");

            migrationBuilder.DropForeignKey(
                name: "FK_UserExam_User_UserId",
                table: "UserExam");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserExam",
                table: "UserExam");

            migrationBuilder.AlterColumn<string>(
                name: "ExamId",
                table: "UserExam",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserExam",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "UserExam",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserExam",
                table: "UserExam",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserExam_UserId",
                table: "UserExam",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserExam_Exam_ExamId",
                table: "UserExam",
                column: "ExamId",
                principalTable: "Exam",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserExam_User_UserId",
                table: "UserExam",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserExam_Exam_ExamId",
                table: "UserExam");

            migrationBuilder.DropForeignKey(
                name: "FK_UserExam_User_UserId",
                table: "UserExam");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserExam",
                table: "UserExam");

            migrationBuilder.DropIndex(
                name: "IX_UserExam_UserId",
                table: "UserExam");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserExam");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserExam",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExamId",
                table: "UserExam",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserExam",
                table: "UserExam",
                columns: new[] { "UserId", "ExamId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserExam_Exam_ExamId",
                table: "UserExam",
                column: "ExamId",
                principalTable: "Exam",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserExam_User_UserId",
                table: "UserExam",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
