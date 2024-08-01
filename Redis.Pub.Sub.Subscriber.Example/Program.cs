using StackExchange.Redis;
using System.Runtime.CompilerServices;
using System.Text;

ConnectionMultiplexer connection = await ConnectionMultiplexer.ConnectAsync("");
ISubscriber subscriber = connection.GetSubscriber();

await subscriber.SubscribeAsync("mychannel",(channel,message)=>
{
    Console.WriteLine(message);
});
Console.Read();

