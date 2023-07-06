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

        var cars = new List<Car>();
        while (resultSetIterator.HasMoreResults)
        {
            var response = await resultSetIterator.ReadNextAsync();
            _logger.LogInformation("Cost {RequestCharge}RU/s to read car", response.RequestCharge);
            
            cars.AddRange(response);
        }
        
        if (cars.Count > 0)
            return req.CreateJsonResponse(cars[0]);

        return req.CreateResponse(HttpStatusCode.NotFound);
    }
}