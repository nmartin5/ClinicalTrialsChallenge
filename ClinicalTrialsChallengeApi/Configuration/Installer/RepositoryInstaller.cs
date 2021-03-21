using ClinicalTrialsChallengeApi.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicalTrialsChallengeApi.Configuration.Installer
{
    public static class RepositoryInstaller
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IStudyFieldRepository, StudyFieldRepository>();
            services.AddScoped<IFullStudyRepository, FullStudyRepository>();
        }
    }
}
