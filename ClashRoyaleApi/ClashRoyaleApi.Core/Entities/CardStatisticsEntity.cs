using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClashRoyaleApi.Core.Entities
{
    public class CardStatisticsEntity : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int CardLevel { get; set; }

        public int HitPoints { get; set; }

        public int Damage { get; set; }

        public int DamagePerSecond { get; set; }

        public int DashDamage { get; set; }

        public int AreaDamage { get; set; }

        public int CrownTowerDamage { get; set; }

        public int ChargeDamage { get; set; }

        public int ShieldHitpoints { get; set; }

        public float Duration { get; set; }

        public int HealingPerSecond { get; set; }

        public virtual CardEntity Card { get; set; }
    }
}
