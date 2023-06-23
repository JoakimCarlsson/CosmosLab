using Microsoft.Extensions.DependencyInjection;

namespace CosmosLab.Functions.Extensions;

internal static class HostBuilderExtensions
{
    internal static IHostBuilder ConfigureApplicationServices(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices(services =>
        {
            services.AddCosmosLabEntityFrameWork();
            services.AddHostedService<DatabaseInitializationService>();
        });

        return hostBuilder; 
    }
}