using Microsoft.EntityFrameworkCore;
using DivingCompetitionAPI.Models;

namespace DivingCompetitionAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Judge> Judges { get; set; }
        public DbSet<Diver> Divers { get; set; }
        public DbSet<Dive> Dives { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<CompetitionDiver> CompetitionDivers { get; set; }
        public DbSet<CompetitionJudge> CompetitionJudges { get; set; }
        public DbSet<DiverDive> DiverDives { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            // Configuración de las claves primarias compuestas y relaciones

            modelBuilder.Entity<CompetitionDiver>()
                .HasKey(cd => new { cd.CompetitionId, cd.DiverId });

            modelBuilder.Entity<CompetitionJudge>()
                .HasKey(cj => new { cj.CompetitionId, cj.JudgeId });

            modelBuilder.Entity<DiverDive>()
                .HasKey(dd => new { dd.DiverId, dd.DiveId, dd.CompetitionId });

            modelBuilder.Entity<Score>()
                .HasKey(s => s.ScoreId);

            // Asegurar que no haya duplicados en Dive
            modelBuilder.Entity<Dive>()
                .HasIndex(d => new { d.DiveCode, d.Group, d.Height })
                .IsUnique();

            // Asegurar que no haya duplicados en Score
            modelBuilder.Entity<Score>()
                .HasIndex(s => new { s.JudgeId, s.DiverId, s.DiveId, s.CompetitionId })
                .IsUnique();

            // Asegurar que no haya duplicados en DiverDive
            modelBuilder.Entity<DiverDive>()
                .HasIndex(dd => new { dd.DiverId, dd.DiveId, dd.CompetitionId })
                .IsUnique();

            // Relaciones entre entidades

            modelBuilder.Entity<CompetitionDiver>()
                .HasOne(cd => cd.Competition)
                .WithMany(c => c.CompetitionDivers)
                .HasForeignKey(cd => cd.CompetitionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CompetitionDiver>()
                .HasOne(cd => cd.Diver)
                .WithMany(d => d.CompetitionDivers)
                .HasForeignKey(cd => cd.DiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CompetitionJudge>()
                .HasOne(cj => cj.Competition)
                .WithMany(c => c.CompetitionJudges)
                .HasForeignKey(cj => cj.CompetitionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CompetitionJudge>()
                .HasOne(cj => cj.Judge)
                .WithMany(j => j.CompetitionJudges)
                .HasForeignKey(cj => cj.JudgeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DiverDive>()
                .HasOne(dd => dd.Diver)
                .WithMany(d => d.DiverDives)
                .HasForeignKey(dd => dd.DiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DiverDive>()
                .HasOne(dd => dd.Dive)
                .WithMany(dv => dv.DiverDives)
                .HasForeignKey(dd => dd.DiveId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DiverDive>()
                .HasOne(dd => dd.Competition)
                .WithMany(c => c.DiverDives)
                .HasForeignKey(dd => dd.CompetitionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Score>()
                .HasOne(s => s.DiverDive)
                .WithMany(dd => dd.Scores)
                .HasForeignKey(s => new { s.DiverId, s.DiveId, s.CompetitionId })
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Score>()
                .HasOne(s => s.Judge)
                .WithMany(j => j.Scores)
                .HasForeignKey(s => s.JudgeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
