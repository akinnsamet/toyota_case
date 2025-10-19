using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Toyota.Api.Authorization;
using Toyota.Application.Api;
using Toyota.Application.IoC;
using Toyota.Application.Logging;
using Toyota.Business.Common.IoC;
using Toyota.Data.Common.IoC;
using Toyota.Data.Context.Toyota;
using Toyota.Entities.SwaggerExample;
using Toyota.Services.Common.IoC;
using Toyota.Shared.Utilities;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics;
using System.Globalization;

using System.Text.Json.Serialization;

Activity.DefaultIdFormat = ActivityIdFormat.W3C;
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://+:80");

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


ApiConfiguration.Configuration = configuration;

builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEntityFrameworkSqlServer().AddDbContext<IToyotaDbContext, ToyotaDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), opt => opt.CommandTimeout(60)));
 

builder.Services.AddEndpointsApiExplorer();

#region JWT
var audienceConfig = configuration.GetSection("Audience");
var xmlKey = File.ReadAllText(@"Authorization//rsa-public-key.xml");
var key = JwtKeyHelper.BuildRsaSigningKey(xmlKey);
var tokenValidationParameters = new TokenValidationParameters()
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = key,
    ValidateIssuer = true,
    ValidIssuer = audienceConfig["Iss"],
    ValidateAudience = true,
    ValidAudience = audienceConfig["Aud"],
    ValidateLifetime = true,
    ClockSkew = TimeSpan.Zero,
    RequireExpirationTime = true
};
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.TokenValidationParameters = tokenValidationParameters;
});
builder.Services.AddAuthorization(options =>
{
    var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
        JwtBearerDefaults.AuthenticationScheme);

    defaultAuthorizationPolicyBuilder =
        defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

    options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
});

#endregion

builder.Services.AddBusinessLayer();
builder.Services.AddDataLayer();
builder.Services.AddServicesLayer();
builder.Services.AddApplicationLayer();


builder.Services.AddSwaggerExamplesFromAssemblyOf<LoginRequestExample>();


builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "en", "tr", "es" };
    options.DefaultRequestCulture = new RequestCulture("tr");
    options.SupportedCultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();
    options.SupportedUICultures = options.SupportedCultures;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("Toyota", new OpenApiInfo() { Title = "Toyota API", Version = "v1" });
});


builder.Services.AddHealthChecks();

var app = builder.Build();
app.UseCors(myAllowSpecificOrigins);
app.MapHealthChecks("/health");

app.UseRequestLocalization();

if (configuration.GetSection("Configurations:SwaggerStatus").Value == Constants.SwaggerStatusOpen)
{
    app.UseSwagger().UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Toyota Swagger v1"));
}
 

app.UseAuthorization();

app.UseMiddleware<LogMiddleware>();
app.UseGlobalExceptionHandler();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<IToyotaDbContext>();
    await dbContext.EnsureCreated();
}

app.Run();