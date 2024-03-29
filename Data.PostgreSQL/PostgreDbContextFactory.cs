﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Data.PostgreSql;

public class PostgreDbContextFactory : IDesignTimeDbContextFactory<PostgreDbContext>
{
    public PostgreDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PostgreDbContext>();
        //if (args is null || !args.Any() || string.IsNullOrWhiteSpace(args[0]))
            //throw new InvalidOperationException("Please provide a valid connection string.");

        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=SYSTEM;Database=api_users_db");
        return new PostgreDbContext(optionsBuilder.Options);
    }
}