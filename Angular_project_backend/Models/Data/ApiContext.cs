using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular_project_backend.Models
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        public DbSet<Antwoord> Antwoorden { get; set; }
        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollGebruiker> PollGebruikers { get; set; }
        public DbSet<Stem> Stemmen { get; set; }
        public DbSet<Vriendschap> Vriendschappen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Antwoord>().ToTable("Antwoord");
            modelBuilder.Entity<Gebruiker>().ToTable("Gebruiker");
            modelBuilder.Entity<Poll>().ToTable("Poll");
            modelBuilder.Entity<PollGebruiker>().ToTable("PollGebruiker");
            modelBuilder.Entity<Stem>().ToTable("Stem");
            modelBuilder.Entity<Vriendschap>().ToTable("Vriendschap");
        }
    }
}
