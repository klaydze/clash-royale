using ClashRoyaleApi.Infrastructure.Attributes;

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

        public Arena Arena { get; set; }

        public int Version { get; set; }
    }
}
