
using StackExchange.Redis;
using System.Runtime.CompilerServices;
using System.Text;

ConnectionMultiplexer connection = await ConnectionMultiplexer.ConnectAsync("");
ISubscriber subscriber = connection.GetSubscriber();

//while (true)
//{
//    Console.Write("Mesaj:");
//    string message = Console.ReadLine();
//    await subscriber.PublishAsync("mychannel", message);

//}


//Başı mychannel olanlara 
while (true)
{
    Console.Write("Mesaj:");
    string message = Console.ReadLine();
    await subscriber.PublishAsync("mychannel.*", message);

}
Console.Read();
