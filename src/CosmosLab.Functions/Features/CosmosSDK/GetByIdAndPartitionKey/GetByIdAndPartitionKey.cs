namespace CosmosLab.Functions.Features.CosmosSDK.GetByIdAndPartitionKey;

public class GetByIdAndPartitionKey
{
    private readonly ILogger<GetByIdAndPartitionKey> _logger;
    private readonly Container _container;

    public GetByIdAndPartitionKey(
        ILogger<GetByIdAndPartitionKey> logger,
        CosmosClient cosmosClient
    )
    {
        _logger = logger;
        _container = cosmosClient.GetContainer("CosmosLabTest", "Carss");
    }

    [Function("GetByIdAndPartitionKey")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "cosmos/car/{id}/{make}")]
        HttpRequestData req,
        FunctionContext executionContext,
        string id,
        string make
        )
    {
        var response = await _container.ReadItemAsync<Car>(
            id,
            new PartitionKey(make),
            cancellationToken: CancellationToken.None);

        _logger.LogInformation("Cost {RequestCharge}RU/s to read car", response.RequestCharge);
        
        if (response.StatusCode == HttpStatusCode.OK)
            return req.CreateJsonResponse(response.Resource);

        return req.CreateResponse(HttpStatusCode.NotFound);
    }
}