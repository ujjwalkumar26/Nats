using Contracts;
using Nats.Contracts;
using NATS.Client;

namespace Nats.Subcription;
class NatsListenerService
{
    static void Main(string[] args)
    {
        // Connect to the NATS server
        using IEncodedConnection c = new ConnectionFactory().CreateEncodedConnection("nats-server");
        c.OnSerialize = NatsSerializer.JsonSerializer;
        c.OnDeserialize = NatsSerializer.JsonDeserializer;
        Console.WriteLine("Connected successfully");
        EventHandler<EncodedMessageEventArgs> eventHandler = (sender, args) =>
        {
            // Here, obj is an instance of the object published to
            // this subscriber.  Retrieve it through the
            // ReceivedObject property of the arguments.
            PublishedMessage? message = (PublishedMessage)args.ReceivedObject;

            Console.WriteLine($"Received Message: {message}");
        };

        // Subscribe using the encoded message event handler
        IAsyncSubscription s = c.SubscribeAsync("subject.*", eventHandler);
        while (true) 
        {
            s.Start();
        }
        

    }
}
