using Microsoft.EntityFrameworkCore;
using next4_api.Models;

namespace next4_api.Data
{
    public class DataContext : DbContext
    {

        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseSqlServer(
                connectionString:@"Persist Security Info=False;server=.\SQLEXPRESS2019;database=next4;uid=sa;pwd=sql339023"
            );
        }

    }
}