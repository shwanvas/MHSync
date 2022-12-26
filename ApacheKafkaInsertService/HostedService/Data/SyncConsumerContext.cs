using Microsoft.EntityFrameworkCore;

namespace HostedService.Data
{
    public class SyncConsumerContext : DbContext
    {

        public SyncConsumerContext(DbContextOptions<SyncConsumerContext> options) : base(options) { }

        public DbSet<SyncDataArchived> SyncDataArchived { get; set; } = null!;

    }
}
