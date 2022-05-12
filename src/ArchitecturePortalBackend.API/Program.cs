using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using ArchitecturePortalBackend.API.Middleware;
using ArchitecturePortalBackend.BusinessLogic.DI;
using ArchitecturePortalBackend.DataAccess.DI;
using ArchitecturePortalBackend.VaultProvider;
using Serilog;
using System.Reflection;
using System.Text.Json.Serialization;

const string DATABASE_KEY = "AzureSQL_ArcPortalDb_apt";

var secretName = "infra-ArchitecturePortalBackend";
var secretFileName = "configs/" + secretName;
#if DEBUG
VaultProvider.WriteSecretToFile(secretName, secretFileName);
#endif

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile(secretFileName, false, true);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog(logger);

builder.Services
    .AddBusinessLogicServices()
    .AddDataAccessServices(builder.Configuration.GetConnectionString(DATABASE_KEY));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        //options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Architecture Portal API",
        Description = "Architecture portal API for storing information about the new architecture"
    });

    options.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["controller"]}_{(e.ActionDescriptor as ControllerActionDescriptor)?.ActionName}");
    //options.DescribeAllEnumsAsStrings();
    //options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


/*builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    //options.SerializerOptions.PropertyNameCaseInsensitive = false;
    //options.SerializerOptions.PropertyNamingPolicy = null;
    //options.SerializerOptions.WriteIndented = true;
});*/

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.yaml", "Architecture Portal API v1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();