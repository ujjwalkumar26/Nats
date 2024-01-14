using Contracts;
using Nats.Contracts;
using NATS.Client;

namespace NatsPublisher;
public class NatsWorkerService : IHostedService, IDisposable
{
    private Timer? _timer;
    private readonly ILogger<NatsWorkerService> _logger;
    private readonly ConnectionFactory connectionFactory;
    private IEncodedConnection? serverConnection;
    private readonly MessageProvider messageProvider;
    public NatsWorkerService(ILogger<NatsWorkerService> logger, MessageProvider messageProvider)
    {
        _logger = logger;
        this.connectionFactory = new ConnectionFactory();
        this.messageProvider = messageProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            this.serverConnection = connectionFactory.CreateEncodedConnection("nats-server");
            serverConnection.OnSerialize = NatsSerializer.JsonSerializer;
            serverConnection.OnDeserialize = NatsSerializer.JsonSerializer;

            this._logger.LogInformation("~ Connection established successfully");

            _timer = new Timer(DoWork,
                           "Running",
                           TimeSpan.Zero,
                           TimeSpan.FromSeconds(1));
        }
        catch (Exception ex) {
            this._logger.LogError("Unable to connect to server\n", ex.Message);
            StopAsync(CancellationToken.None).Wait();
            return Task.FromException(ex);
        }


        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        this._logger.LogInformation("Stopping application");
        _timer?.Change(Timeout.Infinite, 0);
        this.serverConnection?.Drain();
        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        _ = Task.Run(async () =>
        {
            this._logger.LogDebug($"Working with state - {state}");
            if (this.serverConnection == null)
            {
                this._logger.LogError("Connection Failed");
                return;
            }
            var messageString = await this.messageProvider.GenerateMockMessage();
            var publishedMessage = new PublishedMessage() 
            { 
                Publisher = typeof(NatsWorkerService).AssemblyQualifiedName ?? string.Empty,
                Content = messageString
            };
            this.serverConnection.Publish("subject.demo", publishedMessage);
            this._logger.LogInformation($"Published: {publishedMessage}");
        });
    }

    public void Dispose()
    {
        this.serverConnection?.Dispose();
        _timer?.Dispose();
        GC.SuppressFinalize(this);
    }
}
