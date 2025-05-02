using StackExchange.Redis;

ConnectionMultiplexer connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync("localhost:1453");

ISubscriber subscriber= connectionMultiplexer.GetSubscriber();

await subscriber.SubscribeAsync("mychannel.*", (channel, message) =>
{
    Console.WriteLine(message);
});

Console.Read();