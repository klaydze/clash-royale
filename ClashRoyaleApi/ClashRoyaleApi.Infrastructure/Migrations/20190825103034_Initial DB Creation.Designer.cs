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
    [Migration("20190825103034_Initial DB Creation")]
    partial class InitialDBCreation
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

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<int>("ElixirCost");

                    b.Property<string>("IdName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Rarity")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.HasIndex("ArenaId");

                    b.ToTable("Cards");
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

            modelBuilder.Entity("ClashRoyaleApi.Core.Entities.CardEntity", b =>
                {
                    b.HasOne("ClashRoyaleApi.Core.Entities.ArenaEntity", "Arena")
                        .WithMany()
                        .HasForeignKey("ArenaId");
                });
#pragma warning restore 612, 618
        }
    }
}
