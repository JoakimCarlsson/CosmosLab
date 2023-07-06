namespace CosmosLab.Functions.Features.EntityFramework.GetByIdAndPartitionKey;

public class EntityFrameworkGetByIdAndPartitionKey
{
    private readonly ILogger<EntityFrameworkGetByIdAndPartitionKey> _logger;
    private readonly CosmosLabDbContext _dbContext;

    public EntityFrameworkGetByIdAndPartitionKey(
        ILogger<EntityFrameworkGetByIdAndPartitionKey> logger,
        CosmosLabDbContext dbContext
    )
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [Function("EntityFrameworkGetByIdAndPartitionKey")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "entityframework/car/{id}/{make}")]
        HttpRequestData req,
        FunctionContext executionContext,
        Guid id,
        string make
    )
    {
        var car = await _dbContext.Cars
            .WithPartitionKey(make)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (car is not null)
            return req.CreateJsonResponse(car);

        return req.CreateResponse(HttpStatusCode.NotFound);
    }
}