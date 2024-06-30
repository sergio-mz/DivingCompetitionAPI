﻿// <auto-generated />
using System;
using DivingCompetitionAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DivingCompetitionAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240630041629_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DivingCompetitionAPI.Models.Competition", b =>
                {
                    b.Property<int>("CompetitionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompetitionId"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompetitionId");

                    b.ToTable("Competitions");
                });

            modelBuilder.Entity("DivingCompetitionAPI.Models.CompetitionDiver", b =>
                {
                    b.Property<int>("CompetitionId")
                        .HasColumnType("int");

                    b.Property<int>("DiverId")
                        .HasColumnType("int");

                    b.HasKey("CompetitionId", "DiverId");

                    b.HasIndex("DiverId");

                    b.ToTable("CompetitionDivers");
                });

            modelBuilder.Entity("DivingCompetitionAPI.Models.CompetitionJudge", b =>
                {
                    b.Property<int>("CompetitionId")
                        .HasColumnType("int");

                    b.Property<int>("JudgeId")
                        .HasColumnType("int");

                    b.HasKey("CompetitionId", "JudgeId");

                    b.HasIndex("JudgeId");

                    b.ToTable("CompetitionJudges");
                });

            modelBuilder.Entity("DivingCompetitionAPI.Models.Dive", b =>
                {
                    b.Property<int>("DiveId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiveId"));

                    b.Property<double>("Difficulty")
                        .HasColumnType("float");

                    b.Property<string>("DiveCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DiveId");

                    b.ToTable("Dives");
                });

            modelBuilder.Entity("DivingCompetitionAPI.Models.Diver", b =>
                {
                    b.Property<int>("DiverId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiverId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DiverId");

                    b.ToTable("Divers");
                });

            modelBuilder.Entity("DivingCompetitionAPI.Models.DiverDive", b =>
                {
                    b.Property<int>("DiverId")
                        .HasColumnType("int");

                    b.Property<int>("DiveId")
                        .HasColumnType("int");

                    b.Property<int>("CompetitionId")
                        .HasColumnType("int");

                    b.HasKey("DiverId", "DiveId", "CompetitionId");

                    b.HasIndex("CompetitionId");

                    b.HasIndex("DiveId");

                    b.ToTable("DiverDives");
                });

            modelBuilder.Entity("DivingCompetitionAPI.Models.Judge", b =>
                {
                    b.Property<int>("JudgeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JudgeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("JudgeId");

                    b.ToTable("Judges");
                });

            modelBuilder.Entity("DivingCompetitionAPI.Models.Score", b =>
                {
                    b.Property<int>("ScoreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ScoreId"));

                    b.Property<int>("DiverDiveCompetitionId")
                        .HasColumnType("int");

                    b.Property<int>("DiverDiveDiveId")
                        .HasColumnType("int");

                    b.Property<int>("DiverDiveDiverId")
                        .HasColumnType("int");

                    b.Property<int>("JudgeId")
                        .HasColumnType("int");

                    b.Property<double>("Points")
                        .HasColumnType("float");

                    b.HasKey("ScoreId");

                    b.HasIndex("JudgeId");

                    b.HasIndex("DiverDiveDiverId", "DiverDiveDiveId", "DiverDiveCompetitionId");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("DivingCompetitionAPI.Models.CompetitionDiver", b =>
                {
                    b.HasOne("DivingCompetitionAPI.Models.Competition", "Competition")
                        .WithMany("CompetitionDivers")
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DivingCompetitionAPI.Models.Diver", "Diver")
                        .WithMany("CompetitionDivers")
                        .HasForeignKey("DiverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Competition");

                    b.Navigation("Diver");
                });

            modelBuilder.Entity("DivingCompetitionAPI.Models.CompetitionJudge", b =>
                {
                    b.HasOne("DivingCompetitionAPI.Models.Competition", "Competition")
                        .WithMany("CompetitionJudges")
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DivingCompetitionAPI.Models.Judge", "Judge")
                        .WithMany("CompetitionJudges")
                        .HasForeignKey("JudgeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Competition");

                    b.Navigation("Judge");
                });

            modelBuilder.Entity("DivingCompetitionAPI.Models.DiverDive", b =>
                {
                    b.HasOne("DivingCompetitionAPI.Models.Competition", "Competition")
                        .WithMany()
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DivingCompetitionAPI.Models.Dive", "Dive")
                        .WithMany("DiverDives")
                        .HasForeignKey("DiveId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DivingCompetitionAPI.Models.Diver", "Diver")
                        .WithMany("DiverDives")
                        .HasForeignKey("DiverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Competition");

                    b.Navigation("Dive");

                    b.Navigation("Diver");
                });

            modelBuilder.Entity("DivingCompetitionAPI.Models.Score", b =>
                {
                    b.HasOne("DivingCompetitionAPI.Models.Judge", "Judge")
                        .WithMany()
                        .HasForeignKey("JudgeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DivingCompetitionAPI.Models.DiverDive", "DiverDive")
                        .WithMany("Scores")
                        .HasForeignKey("DiverDiveDiverId", "DiverDiveDiveId", "DiverDiveCompetitionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DiverDive");

                    b.Navigation("Judge");
                });

            modelBuilder.Entity("DivingCompetitionAPI.Models.Competition", b =>
                {
                    b.Navigation("CompetitionDivers");

                    b.Navigation("CompetitionJudges");
                });

            modelBuilder.Entity("DivingCompetitionAPI.Models.Dive", b =>
                {
                    b.Navigation("DiverDives");
                });

            modelBuilder.Entity("DivingCompetitionAPI.Models.Diver", b =>
                {
                    b.Navigation("CompetitionDivers");

                    b.Navigation("DiverDives");
                });

            modelBuilder.Entity("DivingCompetitionAPI.Models.DiverDive", b =>
                {
                    b.Navigation("Scores");
                });

            modelBuilder.Entity("DivingCompetitionAPI.Models.Judge", b =>
                {
                    b.Navigation("CompetitionJudges");
                });
#pragma warning restore 612, 618
        }
    }
}
