using Microsoft.EntityFrameworkCore;
using next4_api.Models;

namespace next4_api.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions options) : base(options){}

        public DbSet<User> Users { get; set; }        

    }
}