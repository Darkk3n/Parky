using Microsoft.EntityFrameworkCore;
using Parky.Models;

namespace Parky.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
           : base(options)
        {
        }

        public DbSet<NationalPark> NationalPaks { get; set; }
        public DbSet<Trail> Trails { get; set; }
    }
}