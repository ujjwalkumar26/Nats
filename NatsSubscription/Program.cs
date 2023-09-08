using System;
using System.Text;
using NATS.Client;

namespace Nats.Subcription;
class NatsListenerService
{
    static void Main(string[] args)
    {
        // Connect to the NATS server
        using var connection = new ConnectionFactory().CreateConnection();
        var subject = "subject.demo";
        var subscription = connection.SubscribeSync(subject);

        Console.WriteLine($"Listening for messages on subject: {subject}");

        while (true)
        {
            var message = subscription.NextMessage();
            if (message != null)
            {
                string data = Encoding.UTF8.GetString(message.Data);
                Console.WriteLine(data);
            }
        }
    }
}
