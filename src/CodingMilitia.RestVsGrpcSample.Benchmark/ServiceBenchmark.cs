using System.Net.Http;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using CodingMilitia.RestVsGrpcSample.GrpcLib;

namespace CodingMilitia.RestVsGrpcSample.Benchmark
{
    public class ServiceBenchmark
    {
        [Benchmark]
        public async Task RestAsync()
        {
            using (var client = new HttpClient())
            {
                for (var i = 0; i < 100; ++i)
                {
                    await client.GetAsync("http://localhost:5000");
                }
            }
        }

        [Benchmark]
        public async Task GrpcAsync()
        {
            var client = new HelloClient();
            client.Connect();
            for (var i = 0; i < 100; ++i)
            {
                await client.SendMessageAsync();
            }
            await client.ShutdownAsync();
        }
    }
}