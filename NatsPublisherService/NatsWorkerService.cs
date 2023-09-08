using NATS.Client;
using System.Text;

namespace NatsPublisher;
public class NatsWorkerService : IHostedService, IDisposable
{
    private Timer? _timer;
    private readonly ILogger<NatsWorkerService> _logger;
    private readonly ConnectionFactory connectionFactory;
    private IConnection? serverConnection;
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
            this.serverConnection = connectionFactory.CreateConnection();
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
            var message = await this.messageProvider.GenerateMockMessage();
            this.serverConnection.Publish("subject.demo", Encoding.UTF8.GetBytes(message));
            this._logger.LogInformation($"Published: {message}");
        });
    }

    public void Dispose()
    {
        this.serverConnection?.Dispose();
        _timer?.Dispose();
        GC.SuppressFinalize(this);
    }
}
