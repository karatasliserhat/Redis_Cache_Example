using StackExchange.Redis;

namespace Redis.Sentinel.Services
{
    public class RedisService
    {
        static ConfigurationOptions sentinelConfiguration => new()
        {
            EndPoints =
            {
                {"localhost",6383 },
                {"localhost",6384 },
                {"localhost",6385 },
            },
            CommandMap = CommandMap.Sentinel,
            AbortOnConnectFail = false
        };

        static ConfigurationOptions masterOptions => new()
        {

            AbortOnConnectFail = false
        };

        static public async Task<IDatabase> RedisMasterDatabase()
        {
            ConnectionMultiplexer sentinelConnection =
                await ConnectionMultiplexer.SentinelConnectAsync(sentinelConfiguration);

            System.Net.EndPoint masterEndPoint = null;
            foreach (System.Net.EndPoint endPoint in sentinelConnection.GetEndPoints())
            {
                IServer server = sentinelConnection.GetServer(endPoint);
                if (!server.IsConnected)
                    continue;

                masterEndPoint = await server.SentinelGetMasterAddressByNameAsync("mymaster");

                break;
            }

            var localMasterIp = masterEndPoint.ToString() switch
            {
                "172.19.0.2:6379" => "localhost:6379",
                "172.19.0.3:6379" => "localhost:6380",
                "172.19.0.4:6379" => "localhost:6381",
                "172.19.0.5:6379" => "localhost:6382",

            };

            ConnectionMultiplexer connectionMaster = await ConnectionMultiplexer.ConnectAsync(localMasterIp);
            IDatabase database = connectionMaster.GetDatabase();
            return database;
        }
    }
}
