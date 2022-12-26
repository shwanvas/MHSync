using Microsoft.EntityFrameworkCore;

namespace HostedService.Data
{
    public class SyncDbcontext:DbContext
    {

        public SyncDbcontext(DbContextOptions<SyncDbcontext> options) : base(options) { }

        public DbSet<SyncDataArchived> SyncDataArchive { get; set; } = null!;

    }
}
