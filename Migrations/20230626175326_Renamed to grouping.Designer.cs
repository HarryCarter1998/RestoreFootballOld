﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestoreFootball.Data;

#nullable disable

namespace RestoreFootball.Migrations
{
    [DbContext(typeof(RestoreFootballContext))]
    [Migration("20230626175326_Renamed to grouping")]
    partial class Renamedtogrouping
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RestoreFootball.Models.Gameweek", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("GreenScore")
                        .HasColumnType("int");

                    b.Property<int?>("NonBibsScore")
                        .HasColumnType("int");

                    b.Property<int?>("OrangeScore")
                        .HasColumnType("int");

                    b.Property<int?>("YellowScore")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Gameweek");

                    b.HasAnnotation("Relational:JsonPropertyName", "gameweek");
                });

            modelBuilder.Entity("RestoreFootball.Models.GameweekPlayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GameweekId")
                        .HasColumnType("int");

                    b.Property<int?>("GroupingId")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("Team")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameweekId");

                    b.HasIndex("GroupingId");

                    b.HasIndex("PlayerId");

                    b.ToTable("GameweekPlayer");
                });

            modelBuilder.Entity("RestoreFootball.Models.Grouping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("GameweekId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameweekId");

                    b.ToTable("Grouping");
                });

            modelBuilder.Entity("RestoreFootball.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Player");

                    b.HasAnnotation("Relational:JsonPropertyName", "player");
                });

            modelBuilder.Entity("RestoreFootball.Models.GameweekPlayer", b =>
                {
                    b.HasOne("RestoreFootball.Models.Gameweek", "Gameweek")
                        .WithMany("GameweekPlayers")
                        .HasForeignKey("GameweekId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestoreFootball.Models.Grouping", null)
                        .WithMany("GameweekPlayers")
                        .HasForeignKey("GroupingId");

                    b.HasOne("RestoreFootball.Models.Player", "Player")
                        .WithMany("GameweekPlayers")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gameweek");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("RestoreFootball.Models.Grouping", b =>
                {
                    b.HasOne("RestoreFootball.Models.Gameweek", null)
                        .WithMany("Groupings")
                        .HasForeignKey("GameweekId");
                });

            modelBuilder.Entity("RestoreFootball.Models.Gameweek", b =>
                {
                    b.Navigation("GameweekPlayers");

                    b.Navigation("Groupings");
                });

            modelBuilder.Entity("RestoreFootball.Models.Grouping", b =>
                {
                    b.Navigation("GameweekPlayers");
                });

            modelBuilder.Entity("RestoreFootball.Models.Player", b =>
                {
                    b.Navigation("GameweekPlayers");
                });
#pragma warning restore 612, 618
        }
    }
}