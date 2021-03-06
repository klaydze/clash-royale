﻿// <auto-generated />
using System;
using ClashRoyaleApi.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ClashRoyaleApi.Infrastructure.Migrations
{
    [DbContext(typeof(ClashRoyaleContext))]
    [Migration("20190903090210_Add StringLength to some properties")]
    partial class AddStringLengthtosomeproperties
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ClashRoyaleApi.Core.Entities.ArenaEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IdName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<int>("MinimumTrophies");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<int>("Version");

                    b.Property<int>("VictoryGold");

                    b.HasKey("Id");

                    b.ToTable("Arenas");
                });

            modelBuilder.Entity("ClashRoyaleApi.Core.Entities.CardEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ArenaId");

                    b.Property<int>("Count");

                    b.Property<string>("DashRange")
                        .HasMaxLength(20);

                    b.Property<float>("DeployTime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<int>("ElixirCost");

                    b.Property<float>("HitSpeed");

                    b.Property<string>("IdName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<int>("Lifetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<int>("ProjectileRange");

                    b.Property<float>("Radius");

                    b.Property<string>("Range")
                        .HasMaxLength(20);

                    b.Property<string>("Rarity")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Speed")
                        .HasMaxLength(20);

                    b.Property<string>("Targets")
                        .HasMaxLength(20);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.HasIndex("ArenaId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("ClashRoyaleApi.Core.Entities.CardSecondarySkillsEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CardId");

                    b.Property<int>("Count");

                    b.Property<float>("HitSpeed");

                    b.Property<string>("Name")
                        .HasMaxLength(30);

                    b.Property<string>("Range")
                        .HasMaxLength(30);

                    b.Property<float>("SpawnSpeed");

                    b.Property<string>("Speed")
                        .HasMaxLength(30);

                    b.Property<string>("Targets")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.ToTable("CardSecondarySkills");
                });

            modelBuilder.Entity("ClashRoyaleApi.Core.Entities.CardStatisticsEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AreaDamage");

                    b.Property<int?>("CardId");

                    b.Property<int>("CardLevel");

                    b.Property<int>("ChargeDamage");

                    b.Property<int>("CrownTowerDamage");

                    b.Property<int>("Damage");

                    b.Property<int>("DamagePerSecond");

                    b.Property<int>("DashDamage");

                    b.Property<float>("Duration");

                    b.Property<int>("HealingPerSecond");

                    b.Property<int>("HitPoints");

                    b.Property<int>("ShieldHitpoints");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.ToTable("CardStatistics");
                });

            modelBuilder.Entity("ClashRoyaleApi.Core.Entities.CardsUnlockPerArenaEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArenaId");

                    b.Property<int>("CardId");

                    b.HasKey("Id");

                    b.ToTable("CardsUnlockPerArena");
                });

            modelBuilder.Entity("ClashRoyaleApi.Core.Entities.ChestEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IdName")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.ToTable("Chests");
                });

            modelBuilder.Entity("ClashRoyaleApi.Core.Entities.ChestsUnlockPerArenaEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArenaId");

                    b.Property<int>("CardsCount");

                    b.Property<int>("ChestId");

                    b.Property<int>("GemCost");

                    b.Property<int?>("LeagueId");

                    b.Property<int>("MaximumGold");

                    b.Property<int>("MinimumEpicCardsCount");

                    b.Property<int>("MinimumGold");

                    b.Property<int>("MinimumLegendaryCardsCount");

                    b.Property<int>("MinimumRareCardsCount");

                    b.Property<int?>("NumberOfChoices");

                    b.Property<int>("UnlockCost");

                    b.Property<int>("UnlockTime");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.ToTable("ChestsUnlockPerArena");
                });

            modelBuilder.Entity("ClashRoyaleApi.Core.Entities.LeagueEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IdName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<int>("MinimumTrophies");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<int>("Version");

                    b.Property<int>("VictoryGold");

                    b.HasKey("Id");

                    b.ToTable("Leagues");
                });

            modelBuilder.Entity("ClashRoyaleApi.Core.Entities.CardEntity", b =>
                {
                    b.HasOne("ClashRoyaleApi.Core.Entities.ArenaEntity", "Arena")
                        .WithMany()
                        .HasForeignKey("ArenaId");
                });

            modelBuilder.Entity("ClashRoyaleApi.Core.Entities.CardSecondarySkillsEntity", b =>
                {
                    b.HasOne("ClashRoyaleApi.Core.Entities.CardEntity", "Card")
                        .WithMany()
                        .HasForeignKey("CardId");
                });

            modelBuilder.Entity("ClashRoyaleApi.Core.Entities.CardStatisticsEntity", b =>
                {
                    b.HasOne("ClashRoyaleApi.Core.Entities.CardEntity", "Card")
                        .WithMany()
                        .HasForeignKey("CardId");
                });
#pragma warning restore 612, 618
        }
    }
}
