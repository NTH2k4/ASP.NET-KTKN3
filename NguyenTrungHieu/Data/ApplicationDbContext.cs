using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NguyenTrungHieu.Models;

namespace NguyenTrungHieu.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<NguyenTrungHieu.Models.Performance> Performance { get; set; } = default!;
        public DbSet<NguyenTrungHieu.Models.Theater> Theater { get; set; } = default!;
    }
}
