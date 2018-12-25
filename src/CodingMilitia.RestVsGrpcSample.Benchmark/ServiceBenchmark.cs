using System;
using System.Net.Http;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using CodingMilitia.RestVsGrpcSample.GrpcLib;

namespace CodingMilitia.RestVsGrpcSample.Benchmark
{
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
        public async Task RestAsync()
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
        public async Task GrpcAsync()
        {
            for (var i = 0; i < Iterations; ++i)
            {
                var result = await _grpcClient.SendMessageAsync();

                if (result != ExpectedResponse)
                {
                    throw new Exception("Response is not what's expected!");
                }
            }
        }
    }
}