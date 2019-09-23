using System.ComponentModel.DataAnnotations;

namespace ClashRoyaleApi.Core.Entities
{
    public class CardSecondarySkillsEntity : IEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Targets { get; set; }

        public float HitSpeed { get; set; }

        public int Count { get; set; }

        [StringLength(30)]
        public string Range { get; set; }

        [StringLength(30)]
        public string Speed { get; set; }

        public float SpawnSpeed { get; set; }

        public virtual CardEntity Card { get; set; }
    }
}