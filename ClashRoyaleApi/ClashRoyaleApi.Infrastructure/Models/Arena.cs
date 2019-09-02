using ClashRoyaleApi.Infrastructure.Attributes;

namespace ClashRoyaleApi.Infrastructure.Models
{
    public class Arena
    {
        [Sortable]
        public int Id { get; set; }

        public string IdName { get; set; }

        [Sortable]
        public string Name { get; set; }

        [Sortable]
        public int VictoryGold { get; set; }

        [Sortable]
        public int MinimumTrophies { get; set; }

        public int Version { get; set; }
    }
}
