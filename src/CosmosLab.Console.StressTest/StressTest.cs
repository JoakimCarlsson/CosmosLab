using System.Collections.Concurrent;
using System.Diagnostics;

namespace CosmosLab.Console.StressTest;

internal sealed class StressTest
{
    private readonly HttpClient _client = new();
    private const int MaxRequestsPerSecond = 500;
    private readonly SemaphoreSlim _semaphore = new(MaxRequestsPerSecond, MaxRequestsPerSecond);
    
    private int _successfulResponses;
    private int _failedResponses;
    private readonly ConcurrentBag<double> _responseTimes = new();

    private readonly Stopwatch _stopwatch = new Stopwatch();
    
    public async Task RunAsync()
    {
        var tasks = new List<Task>();
        var cancellationSource = new CancellationTokenSource(new TimeSpan(0,0,1,0));
            
        _ = RefillBucketAsync(cancellationSource.Token);
        _ = PrintStatisticsAsync(cancellationSource.Token);

        while (cancellationSource.Token.IsCancellationRequested is false)
        {
            await _semaphore.WaitAsync(cancellationSource.Token);
            tasks.Add(SendRequestAsync(cancellationSource.Token));
        }

        await Task.WhenAll(tasks);
    }

    private async Task SendRequestAsync(CancellationToken cancellationToken)
    {
        _stopwatch.Restart();
        var response = await _client.PostAsync("http://localhost:7071/api/entityframework/car", null, cancellationToken);
        _stopwatch.Stop();

        _responseTimes.Add(_stopwatch.Elapsed.TotalMilliseconds);

        if (response.IsSuccessStatusCode)
        {
            Interlocked.Increment(ref _successfulResponses);
        }
        else
        {
            Interlocked.Increment(ref _failedResponses);
        }
    }

    private async Task RefillBucketAsync(CancellationToken cancellationToken)
    {
        while (cancellationToken.IsCancellationRequested is false)
        {
            await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            for (var i = 0; i < MaxRequestsPerSecond; i++)
            {
                _semaphore.Release();
            }
        }
    }

    private async Task PrintStatisticsAsync(CancellationToken cancellationToken)
    {
        while (cancellationToken.IsCancellationRequested is false)
        {
            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            System.Console.Clear();
            System.Console.WriteLine($"Successful responses: {_successfulResponses}");
            System.Console.WriteLine($"Failed responses: {_failedResponses}");

            if (_responseTimes.IsEmpty) continue;
            var averageResponseTime = _responseTimes.Average();
            System.Console.WriteLine($"Average response time: {averageResponseTime} ms");
        }
    }
}