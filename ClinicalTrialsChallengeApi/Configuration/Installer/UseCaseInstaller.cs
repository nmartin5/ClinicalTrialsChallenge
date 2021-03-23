using ClinicalTrialsChallengeApi.Domain.UseCase;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicalTrialsChallengeApi.Configuration.Installer
{
    public static class UseCaseInstaller
    {
        public static void ConfigureUseCases(this IServiceCollection services)
        {
            services.AddScoped<IGetEmailUseCase, GetEmailUseCase>();
            services.AddScoped<ISearchEmailUseCase, SearchEmailUseCase>();
            services.AddScoped<ISendNotificationUseCase, SendNotificationUseCase>();
            services.AddScoped<IGetFullStudyUseCase, GetFullStudyUseCase>();
            services.AddScoped<ISearchFullStudiesUseCase, SearchFullStudiesUseCase>();
        }
    }
}
