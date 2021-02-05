using Microsoft.EntityFrameworkCore.Migrations;

namespace backend_api.Migrations
{
    public partial class addCalorieTargetToDiaryEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CalorieTarget",
                table: "DiaryEntries",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalorieTarget",
                table: "DiaryEntries");
        }
    }
}
