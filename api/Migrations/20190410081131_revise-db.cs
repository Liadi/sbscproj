using Microsoft.EntityFrameworkCore.Migrations;

namespace interview.Migrations
{
    public partial class revisedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ScorePercentage",
                table: "UserExam",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Course",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Course");

            migrationBuilder.AlterColumn<int>(
                name: "ScorePercentage",
                table: "UserExam",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
