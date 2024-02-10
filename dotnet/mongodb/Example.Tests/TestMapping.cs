using Example.Types;
using MongoDB.Driver;
using Testcontainers.MongoDb;

namespace Example.Tests;

public class TestMapping
{
    private readonly MongoDbContainer _container = new MongoDbBuilder().Build();
    public TestMapping()
    {
        _container.StartAsync().Wait();
    }
    [Fact]
    public void Test1()
    {
        var client = new MongoClient(_container.GetConnectionString());
        var database = client.GetDatabase("events");
        var collection = database.GetCollection<Event>("events");
        var @event = new Event()
        {
            EventType = EventType.AccountCreated,
            Payload = new AccountCreatedEvent()
            {
                AccountId = Guid.NewGuid().ToString()
            }
        };
        collection.InsertOne(@event);

    }
}