using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class Irrigation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IrrigationDates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayNumber = table.Column<int>(nullable: false),
                    DayName = table.Column<string>(nullable: true),
                    MyFlowersId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IrrigationDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IrrigationDates_MyFlowers_MyFlowersId",
                        column: x => x.MyFlowersId,
                        principalTable: "MyFlowers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IrrigationDates_MyFlowersId",
                table: "IrrigationDates",
                column: "MyFlowersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IrrigationDates");
        }
    }
}
