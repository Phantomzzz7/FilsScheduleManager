using Microsoft.EntityFrameworkCore;
using FSM.Models;

namespace FSM.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

        public DbSet<ScheduleItem> ScheduleItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=fils_schedule.db"); 
        }
    }
}
