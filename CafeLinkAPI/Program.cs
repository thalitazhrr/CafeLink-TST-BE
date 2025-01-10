using CafeLinkAPI;
using CafeLinkAPI.Data;
using Microsoft.EntityFrameworkCore;
using CafeLinkAPI.Extensions;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Options;
using Microsoft.CodeAnalysis.Options;
using MartinCostello.OpenApi;
using System.Text.Json.Serialization;
using BucketQuestAPI.Helpers;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi( opt => {
    opt.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});
builder.Services.AddOpenApiExtensions(options => options.AddServerUrls = true);
builder.Services.AddAppServices(builder.Configuration);
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentityService(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => 
    {
        options.SwaggerEndpoint("/openapi/v1.json", "CafeLinkAPI v1");
    });
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins(builder.Configuration["AllowedOrigins"] ?? throw new InvalidOperationException("AllowedOrigins not found")));

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var logger = services.GetRequiredService<ILogger<Program>>();
logger.LogInformation(builder.Configuration.GetConnectionString("DefaultConnection")); 
logger.LogInformation(builder.Configuration["GoogleClientId"]);
logger.LogInformation(builder.Configuration["GoogleClientSecret"]);
var context = services.GetRequiredService<DataContext>();
// await Seed.SeedCafeAndCoffee(context);
// await Seed.FakeCafeAndCoffee(context);
app.Run();