using System.Runtime.Serialization;

namespace Nats.Contracts;

[DataContract]
public class PublishedMessage
{
    [DataMember]
    public required string Publisher;

    [DataMember]
    public required string Content;

    public override string ToString()
    {
        return $"Publisher: {Publisher}, Content: {Content}";
    }
}