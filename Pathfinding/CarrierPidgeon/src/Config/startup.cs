using CarrierPidgeon.Services;
using CarrierPidgeon.Handlers;
using CarrierPidgeon.Models;
using CarrierPidgeon.Middleware;
using CarrierPidgeon.Serializer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CarrierPidgeon.Config;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Properties"))
            .AddJsonFile("appSettings.json")
            .Build();

        services.AddControllers();
        services.AddScoped<GridServices>();
        services.AddScoped<DroneServices>();
        services.AddScoped<DashboardHandler>();

        services.AddMvc().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new NodePropertiesConverter());
            options.JsonSerializerOptions.Converters.Add(new NodeOriginConverter());
        });

        // Add your other services here

        List<Node> nodes = GridMiddleware.initGrid();
        services.AddSingleton(nodes);

        // Configure JWT authentication using JwtAuthenticationService
            var jwtAuthenticationService = new AuthenticationServices(configuration);
            jwtAuthenticationService.ConfigureAuthentication(services);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
    {
        RouteConfig.Configure(app, environment);

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "dashboard",
                pattern: "dashboard/{action}",
                defaults: new { controller = "Dashboard_Controller" }
            );

            endpoints.MapControllerRoute(
                name: "drone",
                pattern: "drone/{action}",
                defaults: new { controller = "Drone_Controller" }
            );

            // Add other endpoint mappings here
        });

        // Add other middleware and configurations here
    }

}