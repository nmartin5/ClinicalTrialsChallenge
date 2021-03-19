using ClinicalTrialsChallengeApi.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ClinicalTrialsChallengeApi.Configuration.Installer
{
    public static class OpenApiInstaller
    {
        public static void ConfigureOpenApi(this IServiceCollection services)
        {
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = Assembly.GetEntryAssembly().GetName().Name;
                    document.Info.Description = "API Proxy for ClinicalTrials API - https://clinicaltrials.gov/api/gui/ref/api_urls";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Nick Martin",
                        Email = "nmartin5@buffalo.edu",
                        Url = "https://github.com/nmartin5/ClinicalTrialsChallenge"
                    };
                };
            });
        }
    }

    public static class RepositoryInstaller
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IStudyFieldRepository, StudyFieldRepository>();
        }
    }
}
