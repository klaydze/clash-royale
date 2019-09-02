using System.ComponentModel.DataAnnotations;

namespace ClashRoyaleApi.Core.Entities
{
    public class CardsUnlockPerArenaEntity : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int ArenaId { get; set; }

        public int CardId { get; set; }
    }
}
