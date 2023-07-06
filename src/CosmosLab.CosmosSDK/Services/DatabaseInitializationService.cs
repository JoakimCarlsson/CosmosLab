namespace CosmosLab.CosmosSDK.Services;

internal sealed class DatabaseInitializationService : IHostedService
{
    private readonly CosmosClient _cosmosClient;

    public DatabaseInitializationService(CosmosClient cosmosClient)
    {
        _cosmosClient = cosmosClient;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var databaseName = "CosmosLabTest";
        var containerName = "Carss";
        
        var indexingPolicy = new IndexingPolicy
        {
            Automatic = true,
            IndexingMode = IndexingMode.Consistent
        };

        var containerProperties = new ContainerProperties(containerName, "/make")
        {
            IndexingPolicy = indexingPolicy,
        };

        indexingPolicy.IncludedPaths.Add(new IncludedPath { Path = "/make/*" });
        indexingPolicy.ExcludedPaths.Add(new ExcludedPath { Path = "/*" });

        var database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(
            databaseName,
            cancellationToken: cancellationToken
        );

        await database.Database.CreateContainerIfNotExistsAsync(
            containerProperties,
            cancellationToken: cancellationToken
        );
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}