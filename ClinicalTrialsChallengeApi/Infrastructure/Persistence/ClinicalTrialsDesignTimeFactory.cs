using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace ClinicalTrialsChallengeApi.Infrastructure.Persistence
{
    public class ClinicalTrialsDesignTimeFactory : IDesignTimeDbContextFactory<ClinicalTrialsDbContext>
    {
        public ClinicalTrialsDbContext CreateDbContext(string[] args)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables();

            if (environment.Equals("Development"))
            {
                config.AddUserSecrets(Assembly.GetExecutingAssembly());
            }
            var builtConfig = config.Build();

            var optionsBuilder = new DbContextOptionsBuilder<ClinicalTrialsDbContext>();
            var connectionString = builtConfig.GetConnectionString(nameof(ClinicalTrialsDbContext));

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                var projectRoot = AppDomain.CurrentDomain.BaseDirectory;
                var dbFullPath = Path.Join(projectRoot, "clinical-trials.db");
                connectionString = $"Data Source={dbFullPath}";
            }
            optionsBuilder.UseSqlite(connectionString);

            return new ClinicalTrialsDbContext(optionsBuilder.Options);
        }
    }
}
