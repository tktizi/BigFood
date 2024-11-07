using Aggregator.Services.Interfaces;
using Aggregator.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Serilog;
using HealthChecks.UI.Client;
using Polly;
using Polly.Extensions.Http;

namespace Aggregator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Настройка логування
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console().CreateLogger();

            services.AddHealthChecks() // Реєстрація health check сервісів
                .AddUrlGroup(new Uri($"{Configuration["ApiSettings:CatalogService"]}/swagger/index.html"), "CatalogService", HealthStatus.Degraded)
                .AddUrlGroup(new Uri($"{Configuration["ApiSettings:ReviewService"]}/swagger/index.html"), "ReviewService", HealthStatus.Degraded);

            services.AddHttpClient<ICatalogService, CatalogService>(c =>
                c.BaseAddress = new Uri(Configuration["ApiSettings:CatalogService"]))
                .AddPolicyHandler(GetRetryPolicy()) // Додати політику повтору
                .AddPolicyHandler(GetCircuitBreakerPolicy()); // Додати політику розриву

            services.AddHttpClient<IReviewService, ReviewService>(c =>
                c.BaseAddress = new Uri(Configuration["ApiSettings:ReviewService"]))
                .AddPolicyHandler(GetRetryPolicy()) // Додати політику повтору
                .AddPolicyHandler(GetCircuitBreakerPolicy()); // Додати політику розриву

            services.AddControllers();

            // Додати CORS, якщо потрібно
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Aggregator API", Version = "v1" });
            });
        }

        // Цей метод викликається під час виконання програми. 
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aggregator API v1"));
            }

            app.UseRouting();

            // Додати CORS
            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(
                    retryCount: 5,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (exception, retryCount, context) =>
                    {
                        Log.Error($"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}, due to: {exception}.");
                    });
        }


        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 5,
                    durationOfBreak: TimeSpan.FromSeconds(30)
                );
        }
    }
}