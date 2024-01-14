using NatsPublisher;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<NatsWorkerService>();
        services.AddScoped<MessageProvider>();
    })
    .Build();

host.Run();
