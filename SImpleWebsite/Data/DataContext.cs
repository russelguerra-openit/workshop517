using Microsoft.EntityFrameworkCore;
using SImpleWebsite.Models;

namespace SImpleWebsite.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }


        public DbSet<Song> Songs { get; set; }
    }
}
