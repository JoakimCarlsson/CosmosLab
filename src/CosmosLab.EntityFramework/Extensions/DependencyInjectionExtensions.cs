namespace CosmosLab.EntityFramework.Extensions;

public static class DependencyInjectionExtensions
{
    private static IServiceCollection CosmosEntityFrameworkInitialization(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<CosmosLabDbContext>(options =>
        {
            options.UseCosmos(
                "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
                "CosmosLabEntityFramework"
                );
            options.EnableSensitiveDataLogging();
        });
        return serviceCollection;
    }

    public static IServiceCollection AddCosmosEntityFrameWorkDb(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .CosmosEntityFrameworkInitialization()
            .AddHostedService<DatabaseInitializationService>();
        
        return serviceCollection;
    }
}