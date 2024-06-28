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
    }
}
