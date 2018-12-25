using System;
using System.Net.Http;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using CodingMilitia.RestVsGrpcSample.GrpcLib;
using Newtonsoft.Json;

namespace CodingMilitia.RestVsGrpcSample.Benchmark
{
    [RankColumn, MemoryDiagnoser]
    public class ServiceBenchmark
    {
        private const int Iterations = 1000;
        private const string ExpectedResponse = "Hello World!";

        private HttpClient _httpClient;
        private HelloClient _grpcClient;

        [GlobalSetup]
        public void Setup()
        {
            _httpClient = new HttpClient();
            _grpcClient = new HelloClient();
            _grpcClient.Connect();
        }

        [GlobalCleanup]
        public async Task CleanupAsync()
        {
            _httpClient.Dispose();
            await _grpcClient.ShutdownAsync();
        }

        [Benchmark]
        public async Task RestStringAsync()
        {
            for (var i = 0; i < Iterations; ++i)
            {
                var result = await _httpClient.GetStringAsync("http://localhost:5000");

                if (result != ExpectedResponse)
                {
                    throw new Exception("Response is not what's expected!");
                }
            }
        }
        
        [Benchmark]
        public async Task RestJsonAsync()
        {
            for (var i = 0; i < Iterations; ++i)
            {
                var result = await _httpClient.GetStringAsync("http://localhost:5000/json");
                var parsedResult = JsonConvert.DeserializeObject<JsonHelloResponse>(result);
                
                if (parsedResult.Hello != ExpectedResponse)
                {
                    throw new Exception("Response is not what's expected!");
                }
            }
        }

        [Benchmark]
        public async Task GrpcAsync()
        {
            for (var i = 0; i < Iterations; ++i)
            {
                var result = await _grpcClient.SendMessageAsync();

                if (result.Hello != ExpectedResponse)
                {
                    throw new Exception("Response is not what's expected!");
                }
            }
        }
    }
}