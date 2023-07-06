namespace CosmosLab.Functions.Features.EntityFramework.Add;

public class AddCarEntityFrameworkFunction
{
    private readonly ILogger<AddCarEntityFrameworkFunction> _logger;
    private readonly CosmosLabDbContext _dbContext;

    public AddCarEntityFrameworkFunction(
        ILogger<AddCarEntityFrameworkFunction> logger,
        CosmosLabDbContext dbContext
        )
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    
    [Function("AddCarEntityFrameworkFunction")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "entityframework/car")] HttpRequestData req,
        FunctionContext executionContext,
        CancellationToken cancellationToken)
    {
        try
        {
            var testCar = Car.GenerateRandomCar();
            await _dbContext.Cars.AddAsync(testCar, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        
            return req.CreateResponse(HttpStatusCode.OK);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error adding car");
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
