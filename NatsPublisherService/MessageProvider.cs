namespace NatsPublisher;
public class MessageProvider
{
    #pragma warning disable CA1822 
    public async Task<string> GenerateMockMessage()
    #pragma warning restore CA1822
    {
        var dataStore = new List<string>
        {
            "Syntax",
            "Variable",
            "Class",
            "Method",
            "Inheritance",
            "Polymorphism",
            "Encapsulation",
            "Abstraction",
            "Interface",
            "Delegate",
            "Event",
            "Exception",
            "Namespace",
            "Collection",
            "LINQ",
            "Keyword",
            "Compiler",
            "Framework",
            "Nullable",
            "Asynchronous",
            "Thread",
            "GarbageCollection",
            "Debugging",
            "Assembly",
            "Reflection",
            "Attribute",
            "Property",
            "Constructor",
            "Generic",
            "Operator",
            "Typecasting",
            "Enum",
            "AccessModifier",
            "Serializable",
            "Dispose",
            "ExceptionHandling",
            "StringManipulation",
            "WindowsForms",
            "WebAPI",
            "EntityFramework",
            "DependencyInjection",
            "UnitTesting",
            "Serialization",
            "DesignPattern",
            "LambdaExpression",
            "NullableTypes",
            "Indexer",
            "EventHandling",
            "MemoryManagement",
            "Dynamic",
            "CompilerDirective",
            "JITCompilation"
        };

        Random random = new();
        int randomIndex = random.Next(0, dataStore.Count);
        string randomWord = dataStore[randomIndex];
        return await Task.FromResult(randomWord);
    }
}