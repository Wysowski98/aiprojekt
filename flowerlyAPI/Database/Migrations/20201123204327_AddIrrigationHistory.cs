using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class AddIrrigationHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IrrigationHistorie",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MyFlowerId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true),
                    IrrigationDate = table.Column<DateTime>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IrrigationHistorie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IrrigationHistorie_MyFlowers_MyFlowerId",
                        column: x => x.MyFlowerId,
                        principalTable: "MyFlowers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IrrigationHistorie_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IrrigationHistorie_MyFlowerId",
                table: "IrrigationHistorie",
                column: "MyFlowerId");

            migrationBuilder.CreateIndex(
                name: "IX_IrrigationHistorie_UserId",
                table: "IrrigationHistorie",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IrrigationHistorie");
        }
    }
}
