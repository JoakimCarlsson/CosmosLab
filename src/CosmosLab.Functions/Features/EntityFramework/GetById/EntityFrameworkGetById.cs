using Microsoft.EntityFrameworkCore;

namespace CosmosLab.Functions.Features.EntityFramework.GetById;

public class EntityFrameworkGetById
{
    private readonly ILogger<EntityFrameworkGetById> _logger;
    private readonly CosmosLabDbContext _dbContext;

    public EntityFrameworkGetById(
        ILogger<EntityFrameworkGetById> logger,
        CosmosLabDbContext dbContext
    )
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [Function("EntityFrameworkGetById")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "entityframework/car/{id}")]
        HttpRequestData req,
        FunctionContext executionContext,
        Guid id
    )
    {
        var car = await _dbContext.Cars
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (car is not null)
            return req.CreateJsonResponse(car);

        return req.CreateResponse(HttpStatusCode.NotFound);
    }
}