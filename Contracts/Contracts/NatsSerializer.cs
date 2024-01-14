using Nats.Contracts;
using System.Runtime.Serialization.Json;

namespace Contracts;
public static class NatsSerializer
{
    public static object? JsonDeserializer(byte[] buffer)
    {
        using MemoryStream stream = new();
        var serializer = new DataContractJsonSerializer(typeof(PublishedMessage));
        stream.Write(buffer, 0, buffer.Length);
        stream.Position = 0;
        return serializer.ReadObject(stream);
    }

    public static byte[] JsonSerializer(object obj)
    {
        if (obj == null)
            return Array.Empty<byte>();

        var serializer = new DataContractJsonSerializer(typeof(PublishedMessage));

        using MemoryStream stream = new();
        serializer.WriteObject(stream, obj);
        return stream.ToArray();
    }
}
