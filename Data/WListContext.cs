using Microsoft.EntityFrameworkCore;
using w_list.Models;

namespace w_list.Data
{
    public class WListContext : DbContext
    {
        public WListContext (DbContextOptions<WListContext> options)
            : base(options)
        {
        }

        public DbSet<ForumModel> ForumModel { get; set; }
    }
}