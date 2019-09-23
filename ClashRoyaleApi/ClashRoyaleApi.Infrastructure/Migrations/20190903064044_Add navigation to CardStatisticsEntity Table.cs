using Microsoft.EntityFrameworkCore.Migrations;

namespace ClashRoyaleApi.Infrastructure.Migrations
{
    public partial class AddnavigationtoCardStatisticsEntityTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "CardStatistics",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardStatistics_CardId",
                table: "CardStatistics",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardStatistics_Cards_CardId",
                table: "CardStatistics",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardStatistics_Cards_CardId",
                table: "CardStatistics");

            migrationBuilder.DropIndex(
                name: "IX_CardStatistics_CardId",
                table: "CardStatistics");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "CardStatistics");
        }
    }
}
