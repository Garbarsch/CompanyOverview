using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class CompanyContextFactory : IDesignTimeDbContextFactory<CompanyContext>
{
    public CompanyContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();
        
        var connectionString = "Server=tcp:complytodayrgtest.database.windows.net,1433;Initial Catalog=ComplyTodayRGTest;Persist Security Info=False;User ID=rasmus@rasmusgarbarschgmail.onmicrosoft.com;Password=Callofduty2507!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Authentication=\"Active Directory Password\";";

        var optionsBuilder = new DbContextOptionsBuilder<CompanyContext>()
        .UseSqlServer(connectionString);

        return new CompanyContext(optionsBuilder.Options);
    }
   
}