using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class AddIrrigationHistoryfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IrrigationHistorie_MyFlowers_MyFlowerId",
                table: "IrrigationHistorie");

            migrationBuilder.DropForeignKey(
                name: "FK_IrrigationHistorie_Users_UserId",
                table: "IrrigationHistorie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IrrigationHistorie",
                table: "IrrigationHistorie");

            migrationBuilder.RenameTable(
                name: "IrrigationHistorie",
                newName: "IrrigationHistory");

            migrationBuilder.RenameIndex(
                name: "IX_IrrigationHistorie_UserId",
                table: "IrrigationHistory",
                newName: "IX_IrrigationHistory_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_IrrigationHistorie_MyFlowerId",
                table: "IrrigationHistory",
                newName: "IX_IrrigationHistory_MyFlowerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IrrigationHistory",
                table: "IrrigationHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IrrigationHistory_MyFlowers_MyFlowerId",
                table: "IrrigationHistory",
                column: "MyFlowerId",
                principalTable: "MyFlowers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IrrigationHistory_Users_UserId",
                table: "IrrigationHistory",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IrrigationHistory_MyFlowers_MyFlowerId",
                table: "IrrigationHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_IrrigationHistory_Users_UserId",
                table: "IrrigationHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IrrigationHistory",
                table: "IrrigationHistory");

            migrationBuilder.RenameTable(
                name: "IrrigationHistory",
                newName: "IrrigationHistorie");

            migrationBuilder.RenameIndex(
                name: "IX_IrrigationHistory_UserId",
                table: "IrrigationHistorie",
                newName: "IX_IrrigationHistorie_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_IrrigationHistory_MyFlowerId",
                table: "IrrigationHistorie",
                newName: "IX_IrrigationHistorie_MyFlowerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IrrigationHistorie",
                table: "IrrigationHistorie",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IrrigationHistorie_MyFlowers_MyFlowerId",
                table: "IrrigationHistorie",
                column: "MyFlowerId",
                principalTable: "MyFlowers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IrrigationHistorie_Users_UserId",
                table: "IrrigationHistorie",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
