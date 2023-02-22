using Microsoft.EntityFrameworkCore;

namespace Data.PostgreSql;

public class PostgreDbContext : BaseDbContext
{
    public PostgreDbContext(DbContextOptions<PostgreDbContext> options) : base(options)
    {
    }
}