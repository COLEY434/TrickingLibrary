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
    }
}
