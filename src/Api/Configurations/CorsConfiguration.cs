namespace Cocktail.Api.Configurations;

public static class CorsConfiguration
{
    private const string DefaultCorsPolicy = "_DefaultCORSPolicy";

    public static IServiceCollection ConfigureCorsPolicy(this IServiceCollection services, string[] origins = null)
    {
        services.AddCors(options => options.AddPolicy(DefaultCorsPolicy, builder =>
        {
            builder
                .WithOrigins(origins)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders("Content-Disposition");
        }));
        return services;
    }

    public static IApplicationBuilder UseDefaultCorsPolicy(this IApplicationBuilder app)
    {
        app.UseCors(DefaultCorsPolicy);
        return app;
    }
}