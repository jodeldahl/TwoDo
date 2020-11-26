using TwoDo.Domain;
using Microsoft.EntityFrameworkCore;

namespace TwoDo.Data
{
    class TwoDoContext : DbContext
    {
        public DbSet<TwoDoTask> TwoDoTask { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {      
            string connectionString = "Server=.;Database=TwoDo;Trusted_Connection=True";

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
