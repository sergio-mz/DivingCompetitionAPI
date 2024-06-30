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
            base.OnModelCreating(modelBuilder);

            // Configuración de las claves primarias compuestas y relaciones

            modelBuilder.Entity<CompetitionDiver>()
                .HasKey(cd => new { cd.CompetitionId, cd.DiverId });

            modelBuilder.Entity<CompetitionJudge>()
                .HasKey(cj => new { cj.CompetitionId, cj.JudgeId });

            modelBuilder.Entity<DiverDive>()
                .HasKey(dd => new { dd.DiverId, dd.DiveId, dd.CompetitionId });

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

            modelBuilder.Entity<Score>()
                .HasOne(s => s.DiverDive)
                .WithMany(dd => dd.Scores)
                .HasForeignKey(s => new { s.DiverDiveDiverId, s.DiverDiveDiveId, s.DiverDiveCompetitionId })
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Score>()
                .HasOne(s => s.Judge)
                .WithMany()
                .HasForeignKey(s => s.JudgeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}




/*
using DivingCompetitionAPI.Models;
using Microsoft.EntityFrameworkCore;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar relaciones entre Competition y Diver
            modelBuilder.Entity<Competition>()
                .HasMany(c => c.Divers)
                .WithOne(d => d.Competition)
                .HasForeignKey(d => d.CompetitionId)
                .OnDelete(DeleteBehavior.Restrict); // Restringir eliminación en cascada

            // Configurar relaciones entre Competition y Judge
            modelBuilder.Entity<Competition>()
                .HasMany(c => c.Judges)
                .WithOne(j => j.Competition)
                .HasForeignKey(j => j.CompetitionId)
                .OnDelete(DeleteBehavior.Restrict); // Restringir eliminación en cascada

            // Configurar relaciones entre Diver y Dive
            modelBuilder.Entity<Diver>()
                .HasMany(d => d.Dives)
                .WithOne(dv => dv.Diver)
                .HasForeignKey(dv => dv.DiverId)
                .OnDelete(DeleteBehavior.Restrict); // Restringir eliminación en cascada

            // Configurar relaciones entre Dive y Score
            modelBuilder.Entity<Dive>()
                .HasMany(d => d.Scores)
                .WithOne(s => s.Dive)
                .HasForeignKey(s => s.DiveId)
                .OnDelete(DeleteBehavior.Restrict); // Restringir eliminación en cascada

            // Configurar relaciones entre Judge y Score
            modelBuilder.Entity<Judge>()
                .HasMany(j => j.Scores)
                .WithOne(s => s.Judge)
                .HasForeignKey(s => s.JudgeId)
                .OnDelete(DeleteBehavior.Restrict); // Restringir eliminación en cascada
        }
    }
}
*/