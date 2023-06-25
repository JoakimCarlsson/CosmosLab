namespace CosmosLab.Functions.Features.CosmosSDK.AddBatch;

using Car = CosmosLab.Functions.Features.CosmosSDK.Models.Car;

public class AddCarBatchCosmosFunction
{
    private readonly ILogger<AddCarBatchCosmosFunction> _logger;
    private readonly Container _container;
    
    public AddCarBatchCosmosFunction(
        ILogger<AddCarBatchCosmosFunction> logger,
        CosmosClient cosmosClient
        )
    {
        _logger = logger;
        _container = cosmosClient.GetContainer("CosmosLabTest", "Carss");
    }
    
    [Function("AddCarBatchCosmosFunction")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous,  "post", Route = "cosmos/carbatch")] HttpRequestData req,
        FunctionContext executionContext,
        CancellationToken cancellationToken
        )
    {
        try
        {
            var testCarList = new Faker<Car>()
                .CustomInstantiator(f =>
                    new Car(
                        Guid.NewGuid(),
                        "Tesla",
                        f.Vehicle.Model(),
                        f.Random.Number(1900, 2021),
                        f.Commerce.Color(),
                        f.Random.Number(2, 4)
                    )
                ).Generate(5);

            var partitionKey  = new PartitionKey(testCarList.First().Make);
            var transactionalBatch = _container.CreateTransactionalBatch(partitionKey);

            foreach (var car in testCarList)
                transactionalBatch.CreateItem(car);

            var response = await transactionalBatch.ExecuteAsync(cancellationToken);
            _logger.LogInformation("Took {RequestCharge}RU/s to add car", response.RequestCharge);
            
            return req.CreateResponse(HttpStatusCode.OK);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error adding cars");
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}