using ClashRoyaleApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClashRoyaleApi.Infrastructure
{
    public class ClashRoyaleContext : DbContext
    {
        public ClashRoyaleContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<CardEntity> Cards { get; set; }
        public DbSet<ArenaEntity> Arenas { get; set; }
        public DbSet<ChestEntity> Chests { get; set; }
        public DbSet<LeagueEntity> Leagues { get; set; }
        public DbSet<CardsUnlockPerArenaEntity> CardsUnlockPerArena { get; set; }
        public DbSet<ChestsUnlockPerArenaEntity> ChestsUnlockPerArena { get; set; }
        public DbSet<CardSecondarySkillsEntity> CardSecondarySkills { get; set; }
        public DbSet<CardStatisticsEntity> CardStatistics { get; set; }
    }
}
