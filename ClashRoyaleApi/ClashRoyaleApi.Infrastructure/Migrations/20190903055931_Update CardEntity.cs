using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClashRoyaleApi.Infrastructure.Migrations
{
    public partial class UpdateCardEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardSecondarySkills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Targets = table.Column<string>(nullable: true),
                    HitSpeed = table.Column<float>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Range = table.Column<string>(nullable: true),
                    Speed = table.Column<string>(nullable: true),
                    SpawnSpeed = table.Column<float>(nullable: false),
                    CardId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardSecondarySkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardSecondarySkills_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardSecondarySkills_CardId",
                table: "CardSecondarySkills",
                column: "CardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardSecondarySkills");
        }
    }
}
