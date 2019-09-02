using System.ComponentModel.DataAnnotations;

namespace ClashRoyaleApi.Core.Entities
{
    public class ChestsUnlockPerArenaEntity : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int ArenaId { get; set; }

        public int ChestId { get; set; }

        public int? LeagueId { get; set; }

        public int UnlockTime { get; set; }

        public int UnlockCost { get; set; }

        public int GemCost { get; set; }

        public int MinimumGold { get; set; }

        public int MaximumGold { get; set; }

        public int CardsCount { get; set; }

        public int MinimumLegendaryCardsCount { get; set; }

        public int MinimumEpicCardsCount { get; set; }

        public int MinimumRareCardsCount { get; set; }

        public int? NumberOfChoices { get; set; }

        public int Version { get; set; }
    }
}
