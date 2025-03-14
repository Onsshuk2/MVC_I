using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pv311_mvc_project.Models;
using pv311_mvc_project.Models.Identity;

namespace pv311_mvc_project.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<PromoCode> PromoCodes { get; set; }
}

}
