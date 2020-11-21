using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TrickingLibrary.Api.Models;

namespace TrickingLibrary.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        public DbSet<Trick> Tricks { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<TrickRelationship> TrickRelationships { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TrickCategory> TrickCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TrickCategory>()
                .HasKey(s => new { s.CategoryId, s.TrickId });
            builder.Entity<TrickRelationship>()
                .HasKey(s => new { s.PrerequisiteId, s.ProgressionId });

            builder.Entity<TrickRelationship>()
                .HasOne(s => s.Progression)
                .WithMany(s => s.Prerequisites)
                .HasForeignKey(s => s.ProgressionId);

            builder.Entity<TrickRelationship>()
                .HasOne(s => s.Prerequisite)
                .WithMany(s => s.Progressions)
                .HasForeignKey(s => s.PrerequisiteId);
        }
    }
}
