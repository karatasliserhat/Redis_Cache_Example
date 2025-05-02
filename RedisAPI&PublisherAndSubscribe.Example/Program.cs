using StackExchange.Redis;

ConnectionMultiplexer multiplexer = await ConnectionMultiplexer.ConnectAsync("localhost:1453");


ISubscriber subscriber = multiplexer.GetSubscriber();


while (true)
{
    Console.Write("Mesaj: ");
    string message = Console.ReadLine();

    await subscriber.PublishAsync("mychannel", message);
}