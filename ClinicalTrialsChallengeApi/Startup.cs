using ClinicalTrialsChallengeApi.Configuration;
using ClinicalTrialsChallengeApi.Configuration.Installer;
using ClinicalTrialsChallengeApi.Domain.Factory;
using ClinicalTrialsChallengeApi.Domain.UseCase;
using ClinicalTrialsChallengeApi.Infrastructure;
using ClinicalTrialsChallengeApi.Infrastructure.Client;
using ClinicalTrialsChallengeApi.Infrastructure.Persistence;
using ClinicalTrialsChallengeApi.Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace ClinicalTrialsChallengeApi
{
    public class Startup
    {
        private readonly string AllowAllCors = "_allowAllCors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.ConfigureOpenApi();
            services.AddHttpClient();
            services.InstallDbContext(Configuration);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFullStudiesClient, FullStudiesClient>();
            services.Configure<EmailOptions>(options => Configuration.GetSection(nameof(EmailOptions)).Bind(options));
            services.AddScoped<ISendEmailService, EmailService>();
            services.AddScoped<IVCardSerializer, VCard4Serializer>();
            services.AddScoped<IEmailFactory, EmailFactory>();
            services.ConfigureUseCases();

            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowAllCors,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors(AllowAllCors);
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
