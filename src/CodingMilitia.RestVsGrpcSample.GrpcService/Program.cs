using System;
using System.Threading.Tasks;
using CodingMilitia.RestVsGrpcSample.GrpcLib;

namespace CodingMilitia.RestVsGrpcSample.GrpcService
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            var isServer = args[0].Equals("server");

            if(isServer)
            {
                var server = new HelloServer();
                server.Listen();
                Console.WriteLine("Listening...");
                Console.ReadKey();
                await server.ShutdownAsync();
            }
            else
            {
                var client = new HelloClient();
                client.Connect();
                Console.WriteLine(await client.SendMessageAsync());
                await client.ShutdownAsync();
            }
        }
    }
}
