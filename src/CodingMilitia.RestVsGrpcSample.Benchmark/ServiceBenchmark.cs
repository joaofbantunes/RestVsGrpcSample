using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using CodingMilitia.RestVsGrpcSample.Benchmark.Generated;
using Grpc.Net.Client;

namespace CodingMilitia.RestVsGrpcSample.Benchmark
{
    [RankColumn, MemoryDiagnoser]
    public class ServiceBenchmark
    {
        private const int Iterations = 100;
        private const string ExpectedResponse = "Hello World!";

        private static readonly JsonSerializerOptions JsonSerializerOptions =
            new JsonSerializerOptions{PropertyNameCaseInsensitive = true};

        private HttpClient _httpClient;
        private GrpcChannel _grpcChannel;
        private HelloService.HelloServiceClient _grpcClient;

        [GlobalSetup]
        public void Setup()
        {
            _httpClient = new HttpClient();
            
            // Can't use HTTPS on MacOS, so some shenanigans are needed 
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            _grpcChannel = GrpcChannel.ForAddress("http://localhost:5002");
            _grpcClient =  new HelloService.HelloServiceClient(_grpcChannel);
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _httpClient.Dispose();
            _grpcChannel.Dispose();
        }
       
        [Benchmark(Baseline = true)]
        public async Task RestAsync()
        {
            for (var i = 0; i < Iterations; ++i)
            {
                var stringContent = new StringContent(JsonSerializer.Serialize<object>(new JsonHelloRequest{ Name = "World" }));
                stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = await _httpClient.PostAsync("http://localhost:5000/hello", stringContent);
                
                var parsedResult = await JsonSerializer.DeserializeAsync<JsonHelloResponse>(
                    await result.Content.ReadAsStreamAsync(), 
                    JsonSerializerOptions);
                
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
                var result = await _grpcClient.GetHelloAsync(new HelloRequest{ Name = "World"});

                if (result.Hello != ExpectedResponse)
                {
                    throw new Exception("Response is not what's expected!");
                }
            }
        }
    }
}