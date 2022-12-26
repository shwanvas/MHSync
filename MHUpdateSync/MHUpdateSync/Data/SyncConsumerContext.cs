using MHUpdateSync.Model;
using Microsoft.EntityFrameworkCore;

namespace MHUpdateSync.Data
{
    public class SyncConsumerContext : DbContext
    {
        public SyncConsumerContext(DbContextOptions<SyncConsumerContext> options) : base(options) { }

        public DbSet<SyncDataArchived> SyncDataArchived { get; set; } = null!;
    }
}
