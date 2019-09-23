using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClashRoyaleApi.Infrastructure.Migrations
{
    public partial class AddCardStatisticsEntityTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CardLevel = table.Column<int>(nullable: false),
                    HitPoints = table.Column<int>(nullable: false),
                    Damage = table.Column<int>(nullable: false),
                    DamagePerSecond = table.Column<int>(nullable: false),
                    DashDamage = table.Column<int>(nullable: false),
                    AreaDamage = table.Column<int>(nullable: false),
                    CrownTowerDamage = table.Column<int>(nullable: false),
                    ShieldHitpoints = table.Column<int>(nullable: false),
                    Duration = table.Column<float>(nullable: false),
                    HealingPerSecond = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardStatistics", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardStatistics");
        }
    }
}
