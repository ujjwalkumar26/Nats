using NatsPublisher;

// Neural Autonomic Transport System - For someone who asks full form

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<NatsWorkerService>();
    })
    .Build();

host.Run();
