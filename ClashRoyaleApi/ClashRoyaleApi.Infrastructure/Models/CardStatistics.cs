using System;
using System.Collections.Generic;
using System.Text;

namespace ClashRoyaleApi.Infrastructure.Models
{
    public class CardStatistics
    {
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
    }
}
