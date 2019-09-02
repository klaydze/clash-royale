using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClashRoyaleApi.Core.Entities
{
    public class CardEntity : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "IdName is required")]
        [StringLength(30)]
        public string IdName { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(30)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Rarity is required")]
        [StringLength(20)]
        public string Rarity { get; set; }

        [Required(ErrorMessage = "Type is required")]
        [StringLength(20)]
        public string Type { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500)]
        public string Description { get; set; }

        public int ElixirCost { get; set; }

        //[ForeignKey("Id")]
        //public int ArenaId { get; set; }

        public int Version { get; set; }

        public virtual ArenaEntity Arena { get; set; }
    }
}
