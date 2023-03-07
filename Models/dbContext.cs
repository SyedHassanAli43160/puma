using Microsoft.EntityFrameworkCore;
using puma.Models;

namespace puma.Models
{
    public class dbContext:DbContext
    {
        public dbContext(DbContextOptions<dbContext> options):base(options) { }
        public DbSet<stationCategory>stationCategories { get; set; }
        public virtual DbSet<stationMaster> StationMasters { get; set; }
        //public DbSet<puma.Models.stationMaster> stationMaster { get; set; } = default!;
        public DbSet<puma.Models.categoryMaster> categoryMaster { get; set; } = default!;
    }
}
