using ClashRoyaleApi.Infrastructure.Attributes;
using System.Collections.Generic;

namespace ClashRoyaleApi.Infrastructure.Models
{
    public class Card
    {
        [Sortable]
        public int Id { get; set; }

        public string IdName { get; set; }

        [Sortable(IsDefault = true)]
        [SearchableString]
        public string Name { get; set; }

        [Sortable]
        [SearchableString]
        public string Rarity { get; set; }

        [SearchableString]
        public string Type { get; set; }

        [SearchableString]
        public string Description { get; set; }

        [Sortable]
        [SearchableInteger]
        public int ElixirCost { get; set; }

        public string Targets { get; set; }

        public float HitSpeed { get; set; }

        public int Count { get; set; }

        public string Range { get; set; }

        public string Speed { get; set; }

        public float Radius { get; set; }

        public float DeployTime { get; set; }

        public int Lifetime { get; set; }

        public string DashRange { get; set; }

        public int ProjectileRange { get; set; }

        public Arena Arena { get; set; }

        public int Version { get; set; }

        public List<CardStatistics> CardStatistics { get; set; }
    }
}
