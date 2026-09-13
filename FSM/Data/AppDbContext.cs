using Microsoft.EntityFrameworkCore;
using FSM.Models;
using System.IO;
namespace FSM.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

        public DbSet<ScheduleItem> ScheduleItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "fils_schedule.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}
