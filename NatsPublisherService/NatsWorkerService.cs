namespace NatsPublisher;
public class NatsWorkerService : IHostedService, IDisposable
{
    private Timer? _timer;
    private readonly ILogger<NatsWorkerService> _logger;

    public NatsWorkerService(ILogger<NatsWorkerService> logger)
    {
        _logger = logger;
        // Initialize your external server connection here
        // Replace with your connection logic
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Start the connection when the application starts
        // _serverConnection.Connect();

        // Schedule a timer to read messages every 5 seconds
        _timer = new Timer(DoWork, "Running", TimeSpan.Zero, TimeSpan.FromSeconds(1));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        this._logger.LogInformation("Stopping application");
        // Stop the timer and close the connection when the application stops
        _timer?.Change(Timeout.Infinite, 0);
        //_serverConnection?.Disconnect();

        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        this._logger.LogInformation($"Working with state - {state}");
        // Read messages from the external server and print them
        //var messages = _serverConnection.ReadMessages(); // Implement your message reading logic
        //foreach (var message in messages)
        //{
        //    Console.WriteLine($"Received message: {message}");
        //}
    }

    public void Dispose()
    {
        // Dispose of any resources if needed
        _timer?.Dispose();
        GC.SuppressFinalize(this);
        //_serverConnection?.Dispose();
    }
}
