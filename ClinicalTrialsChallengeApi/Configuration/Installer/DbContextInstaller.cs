using ClinicalTrialsChallengeApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace ClinicalTrialsChallengeApi.Configuration.Installer
{
    public static class DbContextInstaller
    {
        public static void InstallDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ClinicalTrialsDbContext>();
            var connectionString = configuration.GetConnectionString(nameof(ClinicalTrialsDbContext));

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                var projectRoot = AppDomain.CurrentDomain.BaseDirectory;
                var dbFullPath = Path.Join(projectRoot, "clinical-trials.db");
                connectionString = $"Data Source={dbFullPath}";
            }
            optionsBuilder.UseSqlite(connectionString);

            services.AddScoped(_ => new ClinicalTrialsDbContext(optionsBuilder.Options));
        }
    }
}
