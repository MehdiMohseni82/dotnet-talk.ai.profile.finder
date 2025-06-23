using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DotNetTalk.AI.Profile.Finder.Gateways.Sql;

public class AiProfileFinderDesignTimeDbContextFactory : IDesignTimeDbContextFactory<AiProfileFinderDbContext>
{
    public AiProfileFinderDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<AiProfileFinderDbContext>();
        builder.UseSqlServer("Server=127.0.0.1,1533;Database=ai-profilefinder;User Id=sa;Password=halo!IAM5789;MultipleActiveResultSets=true;MultiSubnetFailover=True;TrustServerCertificate=True");

        return new AiProfileFinderDbContext(builder.Options);
    }
}