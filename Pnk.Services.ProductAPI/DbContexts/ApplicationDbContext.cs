using Microsoft.EntityFrameworkCore;
using Pnk.Services.ProductAPI.Models;

namespace Pnk.Services.ProductAPI.DbContexts
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions):base(dbContextOptions)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
