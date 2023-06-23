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
        var databaseName = "CosmosLab";
        var containerName = "Cars";
        
        var database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(
            databaseName,
        cancellationToken: cancellationToken
            );
        
        await database.Database.CreateContainerIfNotExistsAsync(
            containerName, 
            "/make",
            cancellationToken: cancellationToken
            );
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}