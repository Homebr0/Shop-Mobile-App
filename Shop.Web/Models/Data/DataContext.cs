

namespace Shop.Web.Models.Data
{
    using Microsoft.EntityFrameworkCore;
    using Shop.Web.Models.Data.Entities;

    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
