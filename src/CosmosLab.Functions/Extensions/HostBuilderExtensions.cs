namespace CosmosLab.Functions.Extensions;

internal static class HostBuilderExtensions
{
    internal static IHostBuilder ConfigureApplicationServices(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices(services =>
        {
            services.AddCosmosEntityFrameWorkDb();
            services.AddCosmosDb();
        });

        return hostBuilder; 
    }
}