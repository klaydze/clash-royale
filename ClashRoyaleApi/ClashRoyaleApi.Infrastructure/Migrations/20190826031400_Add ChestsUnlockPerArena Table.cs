using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClashRoyaleApi.Infrastructure.Migrations
{
    public partial class AddChestsUnlockPerArenaTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CardsUnlockPerArenaEntity",
                table: "CardsUnlockPerArenaEntity");

            migrationBuilder.RenameTable(
                name: "CardsUnlockPerArenaEntity",
                newName: "CardsUnlockPerArena");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CardsUnlockPerArena",
                table: "CardsUnlockPerArena",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ChestsUnlockPerArena",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArenaId = table.Column<int>(nullable: false),
                    ChestId = table.Column<int>(nullable: false),
                    LeagueId = table.Column<int>(nullable: true),
                    UnlockTime = table.Column<int>(nullable: false),
                    UnlockCost = table.Column<int>(nullable: false),
                    GemCost = table.Column<int>(nullable: false),
                    MinimumGold = table.Column<int>(nullable: false),
                    MaximumGold = table.Column<int>(nullable: false),
                    CardsCount = table.Column<int>(nullable: false),
                    MinimumLegendaryCardsCount = table.Column<int>(nullable: false),
                    MinimumEpicCardsCount = table.Column<int>(nullable: false),
                    MinimumRareCardsCount = table.Column<int>(nullable: false),
                    NumberOfChoices = table.Column<int>(nullable: true),
                    Version = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChestsUnlockPerArena", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChestsUnlockPerArena");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CardsUnlockPerArena",
                table: "CardsUnlockPerArena");

            migrationBuilder.RenameTable(
                name: "CardsUnlockPerArena",
                newName: "CardsUnlockPerArenaEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CardsUnlockPerArenaEntity",
                table: "CardsUnlockPerArenaEntity",
                column: "Id");
        }
    }
}
