namespace CosmosLab.Functions.Features.CosmosSDK.Add;

public class AddCarCosmosFunction
{
    private readonly ILogger<AddCarCosmosFunction> _logger;
    private readonly Container _container;

    public AddCarCosmosFunction(
        ILogger<AddCarCosmosFunction> logger,
        CosmosClient cosmosClient
        )
    {
        _logger = logger;
        _container = cosmosClient.GetContainer("CosmosLabTest", "Carss");
    }
    
    [Function("AddCarCosmosFunction")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "cosmos/car")] HttpRequestData req,
        FunctionContext executionContext,
         CancellationToken cancellationToken
        )
    {
        try
        {
            var testCar = Car.GenerateRandomCar();
            var response = await _container.CreateItemAsync(
                testCar,
                new PartitionKey(testCar.Make),
                cancellationToken: cancellationToken);

            _logger.LogInformation("Took {RequestCharge}RU/s to add car", response.RequestCharge);
            return req.CreateResponse(response.StatusCode == HttpStatusCode.Created ? HttpStatusCode.Created : HttpStatusCode.BadRequest);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error adding car");
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}