﻿using StackExchange.Redis;

namespace Redis.Sentinel.Services
{
    public class RedisService
    {
        static ConfigurationOptions sentinelOptions => new()
        {
            EndPoints =
            {
                { "localhost",6383},
                { "localhost",6384},
                { "localhost",6385}
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
            ConnectionMultiplexer sentinelConnection = await ConnectionMultiplexer.SentinelConnectAsync(sentinelOptions);

            System.Net.EndPoint masterEndPoint = null;

            foreach (System.Net.EndPoint endpoint in sentinelConnection.GetEndPoints())
            {
                IServer server = sentinelConnection.GetServer(endpoint);
                if (!server.IsConnected)
                {
                    continue;
                }
                masterEndPoint = await server.SentinelGetMasterAddressByNameAsync("mymaster");
                break;
            }

            var localMasterIp = masterEndPoint.ToString() switch
            {
                //"172.17.0.5:6379" => "localhost:6379",
                //"172.18.0.6:6379" => "localhost:6380",
                //"172.18.0.7:6379" => "localhost:6381",
                //"172.18.0.8:6379" => "localhost:6382"
            };
            ConnectionMultiplexer masterConnection = await ConnectionMultiplexer.ConnectAsync(localMasterIp);

            IDatabase database = masterConnection.GetDatabase();

            return database;

        }
    }


}
