using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [StringLength(20)]
        public string Targets { get; set; }

        public float HitSpeed { get; set; }

        public int Count { get; set; }

        [StringLength(20)]
        public string Range { get; set; }

        [StringLength(20)]
        public string Speed { get; set; }

        public float Radius { get; set; }

        public float DeployTime { get; set; }
        
        public int Lifetime { get; set; }

        [StringLength(20)]
        public string DashRange { get; set; }

        public int ProjectileRange { get; set; }

        public int Version { get; set; }

        public virtual ArenaEntity Arena { get; set; }

        public virtual ICollection<CardStatisticsEntity> CardStatistics { get; set; }
    }
}
