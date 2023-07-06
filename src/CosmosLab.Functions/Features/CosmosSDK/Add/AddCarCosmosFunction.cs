namespace CosmosLab.Functions.Features.CosmosSDK.Add;

using Car = CosmosLab.Functions.Features.CosmosSDK.Models.Car;


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
            var testCar = new Faker<Car>()
                .CustomInstantiator(f =>
                    new Car(
                        Guid.NewGuid(),
                        f.Vehicle.Manufacturer(),
                        f.Vehicle.Model(),
                        f.Random.Number(1900, 2021),
                        f.Commerce.Color(),
                        f.Random.Number(2, 4),
                        f.Random.Number(1, 10),
                        f.Random.Number(1, 10),
                        f.Random.Number(1, 10),
                        f.Random.Number(1, 10),
                        f.Random.Number(1, 10),
                        f.Random.Number(1, 10),
                        f.Random.Number(1, 10),
                        f.Random.Number(1, 10),
                        f.Random.Number(1, 10),
                        f.Random.Number(1, 10),
                        f.Random.Number(1, 10),
                        f.Random.Number(1, 10),
                        f.Random.Number(1, 10),
                        f.Random.Number(1, 100000),
                        f.Random.Number(1, 100000),
                        f.Random.Number(1, 1000000)
                    )
                ).Generate();

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