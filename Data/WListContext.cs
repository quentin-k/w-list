using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using w_list.Models;

namespace w_list.Data
{
    public class WListContext : IdentityDbContext
    {
        public WListContext (DbContextOptions<WListContext> options)
            : base(options)
        {
        }
        public DbSet<MemberModel> MemberModels { get; set; }
    }
}