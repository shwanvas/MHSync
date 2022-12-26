using MHSync.Model;
using Microsoft.EntityFrameworkCore;

namespace MHSync.Data
{
    public class SyncDbContext : DbContext
    {
        public SyncDbContext(DbContextOptions<SyncDbContext> options) : base(options) { }

        public DbSet<SyncDataArchived> SyncDataArchive { get; set; } = null!;

    }
}
