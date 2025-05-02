using StackExchange.Redis;

ConnectionMultiplexer multiplexer = await ConnectionMultiplexer.ConnectAsync("localhost:1453");


ISubscriber subscriber = multiplexer.GetSubscriber();


await subscriber.SubscribeAsync("mychannel", (channel, message) =>
{
    Console.WriteLine(message);
});

Console.Read();