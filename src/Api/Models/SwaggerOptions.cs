namespace Cocktail.Api.Models;

public class SwaggerOptions
{
    public const string Option = "Swagger";
    public string Description { get; set; } = "Api Description";
    public string Version { get; set; } = "v1";
    public string Title { get; set; } = "Swagger Api";
    public bool EnableXmlComments { get; set; } = false;
    public bool EnableMiniProfiler { get; set; } = false;
    public bool EnableOAuthAuthentication { get; set; } = false;
    public bool EnableBasicAuthentication { get; set; } = false;
    public bool EnableMultipartFormData { get; set; } = false;
    public string? AuthorizationUrl { get; set; }
    public string? TokenUrl { get; set; }
    public string? OpenIdClientId { get; set; }
    public string? ApiScopes { get; set; }
    public string? Audience { get; set; }
}