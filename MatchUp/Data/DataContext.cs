using MatchUp.Entities;
using Microsoft.EntityFrameworkCore;

namespace MatchUp.Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<AppUser> Users { get; set; }
    }
}
