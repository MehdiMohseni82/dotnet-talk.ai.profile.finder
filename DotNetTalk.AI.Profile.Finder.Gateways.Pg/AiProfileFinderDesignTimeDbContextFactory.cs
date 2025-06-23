using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DotNetTalk.AI.Profile.Finder.Gateways.Pg;

public class AiProfileFinderDesignTimeDbContextFactory : IDesignTimeDbContextFactory<AiProfileFinderDbContext>
{
    public AiProfileFinderDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<AiProfileFinderDbContext>();
        builder.UseNpgsql("Host=localhost;Port=5432;Database=profilefinderdb;Username=postgres;Password=postgres123", o => o.UseVector());

        return new AiProfileFinderDbContext(builder.Options);
    }
}