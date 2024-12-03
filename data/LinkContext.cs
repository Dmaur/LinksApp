using LinksApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LinksApp.Data
{
    public class LinkContext : DbContext
    {
        public LinkContext(DbContextOptions<LinkContext> options) : base(options)
        {
            
        }

        public DbSet<LinkModel> tblLinks {get; set;} = default!;
        public DbSet<CatModel> tblCategories { get; set; } = default!;
    }
}
