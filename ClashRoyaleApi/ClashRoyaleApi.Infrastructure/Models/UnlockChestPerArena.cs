using System;
using System.Collections.Generic;
using System.Text;

namespace ClashRoyaleApi.Infrastructure.Models
{
    public class UnlockChestPerArena
    {
        public int Id { get; set; }

        public string ArenaName { get; set; }

        public string ChestName { get; set; }

        public string LeagueName { get; set; }

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
