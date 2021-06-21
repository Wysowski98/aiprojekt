using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class addFieldsToIrrigationDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "IrrigationDates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ScheduledJobId",
                table: "IrrigationDates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "IrrigationDates");

            migrationBuilder.DropColumn(
                name: "ScheduledJobId",
                table: "IrrigationDates");
        }
    }
}
