using Identity.MVC.Client.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.MVC.Client.DBContext
{
    /// <summary>
    /// Extending IdentityDbContext of Microsoft AspNET IdentityDbContext.
    /// </summary>
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions):base(dbContextOptions)
        {

        }
    }
}
