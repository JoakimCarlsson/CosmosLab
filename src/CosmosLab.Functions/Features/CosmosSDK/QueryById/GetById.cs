namespace CosmosLab.Functions.Features.CosmosSDK.QueryById;

public class GetById
{
    private readonly ILogger<GetById> _logger;
    private readonly Container _container;

    public GetById(
        ILogger<GetById> logger,
        CosmosClient cosmosClient
    )
    {
        _logger = logger;
        _container = cosmosClient.GetContainer("CosmosLabTest", "Carss");
    }
    
    [Function("GetById")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "cosmos/car/{id}")] HttpRequestData req,
        FunctionContext executionContext,
        string id)
    {
        var queryDefinition = new QueryDefinition("SELECT * FROM c WHERE c.id = @id")
            .WithParameter("@id", id);
        
        var resultSetIterator = _container.GetItemQueryIterator<Car>(queryDefinition);

        while (resultSetIterator.HasMoreResults)
        {
            var response = await resultSetIterator.ReadNextAsync();
            _logger.LogInformation("Cost {RequestCharge}RU/s to read car", response.RequestCharge);
            
            if (response.Count > 0)
                return req.CreateJsonResponse(response.First());
        }

        return req.CreateResponse(HttpStatusCode.NotFound);
    }
}