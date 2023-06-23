namespace CosmosLab.Console.StressTest;

public class StressTest
{
    private readonly HttpClient _client = new();
    private const int MaxRequestsPerSecond = 500;
    private readonly TimeSpan _delayBetweenTokens = TimeSpan.FromSeconds(1.0 / MaxRequestsPerSecond);
    private readonly SemaphoreSlim _semaphore = new(MaxRequestsPerSecond, MaxRequestsPerSecond);
    
    public async Task RunAsync()
    {
        var tasks = new List<Task>();
        var cancellationSource = new CancellationTokenSource(new TimeSpan(0,0,1,0));
            
        _ = RefillBucket(cancellationSource.Token);

        while (!cancellationSource.Token.IsCancellationRequested)
        {
            await _semaphore.WaitAsync(cancellationSource.Token);
            tasks.Add(SendRequest(cancellationSource.Token));
        }

        await Task.WhenAll(tasks);
    }

    private async Task SendRequest(CancellationToken cancellationToken)
    {
        var response = await _client.PostAsync("http://localhost:7071/api/Cosmos/car", null, cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            System.Console.WriteLine("Success");
        }
    }

    private async Task RefillBucket(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(_delayBetweenTokens, cancellationToken);
            _semaphore.Release();
        }
    }
}