using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class AddIrrigationHIstoryField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IrrigationDatesId",
                table: "IrrigationHistory",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IrrigationHistory_IrrigationDatesId",
                table: "IrrigationHistory",
                column: "IrrigationDatesId");

            migrationBuilder.AddForeignKey(
                name: "FK_IrrigationHistory_IrrigationDates_IrrigationDatesId",
                table: "IrrigationHistory",
                column: "IrrigationDatesId",
                principalTable: "IrrigationDates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IrrigationHistory_IrrigationDates_IrrigationDatesId",
                table: "IrrigationHistory");

            migrationBuilder.DropIndex(
                name: "IX_IrrigationHistory_IrrigationDatesId",
                table: "IrrigationHistory");

            migrationBuilder.DropColumn(
                name: "IrrigationDatesId",
                table: "IrrigationHistory");
        }
    }
}
