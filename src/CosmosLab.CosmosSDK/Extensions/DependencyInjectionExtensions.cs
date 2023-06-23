namespace CosmosLab.CosmosSDK.Extensions;

public static class DependencyInjectionExtensions
{
    private static IServiceCollection CosmosClientInitialization(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton(x => 
        {
            var clientBuilder = new CosmosClientBuilder("AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");
            return clientBuilder
                .WithSerializerOptions(new CosmosSerializationOptions
                {
                    Indented = false,
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase,
                })
                .Build();
        });
        return serviceCollection;
    }

    public static IServiceCollection AddCosmosDb(this IServiceCollection serviceCollection)
    {
        serviceCollection.CosmosClientInitialization();
        serviceCollection.AddHostedService<DatabaseInitializationService>();
        return serviceCollection;
    }
}