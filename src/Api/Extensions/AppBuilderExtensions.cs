using Cocktail.Api.Models;
using Microsoft.AspNetCore.HttpOverrides;

namespace Cocktail.Api.Extensions;

public static class AppBuilderExtensions
{
    public static IApplicationBuilder UseSwagger( this IApplicationBuilder app, IConfiguration configuration )
    {
        var swaggerOption = configuration.GetSection( SwaggerOptions.Option ).Get< SwaggerOptions >();

        app.UseForwardedHeaders( new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.All
        } );

        app.Use( ( context, next ) =>
        {
            UseForwardedProtoHeader( context );
            return next();
        } );

        app.UseSwagger();
        return app.UseSwaggerUI( c =>
        {
            c.SwaggerEndpoint( "/swagger/v1/swagger.json", swaggerOption?.Title );
            
            if ( !swaggerOption?.EnableOAuthAuthentication ?? false ) return;
            c.OAuthClientId( swaggerOption?.OpenIdClientId );
            c.OAuthAppName( swaggerOption?.Title );
            c.OAuthUsePkce();
            c.OAuthScopeSeparator( " " );
            if ( !string.IsNullOrEmpty( swaggerOption?.Audience ) )
                c.OAuthAdditionalQueryStringParams( new Dictionary< string, string > { { "audience", swaggerOption.Audience } } );
        } );
    }
    
    private static void UseForwardedProtoHeader( HttpContext context )
    {
        if ( context.Request.Headers.TryGetValue( "X-Forwarded-Proto", out var value ) ) context.Request.Scheme = value.First() ?? string.Empty;
    }
}