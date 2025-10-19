using System.Text.Json.Serialization;
using Toyota.Shared.ApiCall;
using Toyota.Web.Middleware;
using Toyota.Web.Services;

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://+:80");
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


// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All);
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// HttpClient configuration
builder.Services.AddScoped<IApiCall, ApiCall>();
builder.Services.AddHttpClient<IApiService, ApiService>();
builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClientServiceCollection(builder.Configuration);


var app = builder.Build();
app.UseCors(myAllowSpecificOrigins);


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
 
app.UseStaticFiles();

app.UseSession();
app.UseMiddleware<JwtAuthenticationMiddleware>();
app.Use(async (context, next) =>
{
    var path = context.Request.Path.ToString().ToLower();
    if (!path.Contains("/auth/login") && string.IsNullOrEmpty(context.Session.GetString("AccessToken")))
    {
        context.Response.Redirect("/Auth/Login");
        return;
    }
    await next();
});

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
