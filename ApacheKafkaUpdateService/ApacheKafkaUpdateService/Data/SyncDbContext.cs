using Microsoft.EntityFrameworkCore;

namespace ApacheKafkaUpdateService.Data
{
    public class SyncDbContext :DbContext
    {
        public SyncDbContext(DbContextOptions<SyncDbContext> options) : base(options) { }

        public DbSet<SyncDataArchived> SyncDataArchive { get; set; } = null!;
    }
}
