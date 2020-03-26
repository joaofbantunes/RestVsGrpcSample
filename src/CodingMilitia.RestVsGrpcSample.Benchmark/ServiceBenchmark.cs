using System;
using System.Linq;
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
        private const int ComplexCollectionsSize = 50;
        private const string ExpectedResponse = "Hello World!";

        private static readonly JsonSerializerOptions JsonSerializerOptions =
            new JsonSerializerOptions {PropertyNameCaseInsensitive = true};

        private HttpClient _httpClient;
        private GrpcChannel _grpcChannel;
        private HelloService.HelloServiceClient _grpcClient;

        [GlobalSetup]
        public void Setup()
        {
            _httpClient = new HttpClient();
            _grpcChannel = GrpcChannel.ForAddress("https://localhost:5003");
            _grpcClient = new HelloService.HelloServiceClient(_grpcChannel);
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _httpClient.Dispose();
            _grpcChannel.Dispose();
        }

        [Benchmark(Baseline = true)]
        public async Task Rest()
        {
            for (var i = 0; i < Iterations; ++i)
            {
                var stringContent =
                    new StringContent(JsonSerializer.Serialize(new JsonHelloRequest {Name = "World"}));
                stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5001/hello")
                {
                    Content = stringContent
                };
                
                using var responseMessage = await _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);

                var parsedResult = await JsonSerializer.DeserializeAsync<JsonHelloResponse>(
                    await responseMessage.Content.ReadAsStreamAsync(),
                    JsonSerializerOptions);

                if (parsedResult.Hello != ExpectedResponse)
                {
                    throw new Exception("Response is not what's expected!");
                }
            }
        }

        [Benchmark]
        public async Task Grpc()
        {
            for (var i = 0; i < Iterations; ++i)
            {
                var result = await _grpcClient.GetHelloAsync(new HelloRequest {Name = "World"});

                if (result.Hello != ExpectedResponse)
                {
                    throw new Exception("Response is not what's expected!");
                }
            }
        }

        [Benchmark]
        public async Task ComplexRest()
        {
            for (var i = 0; i < Iterations; ++i)
            {
                var request = new JsonComplexHelloRequest
                {
                    Name = "World",
                    SimpleHellos = Enumerable.Range(0, ComplexCollectionsSize)
                        .Select(i => new JsonHelloRequest {Name = "World " + i}),
                    SomeRandomNumbers = Enumerable.Range(0, ComplexCollectionsSize)
                };

                var stringContent = new StringContent(JsonSerializer.Serialize<object>(request));
                stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5001/hello/complex")
                {
                    Content = stringContent
                };
                
                using var responseMessage = await _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);

                var parsed = await JsonSerializer.DeserializeAsync<JsonComplexHelloResponse>(
                    await responseMessage.Content.ReadAsStreamAsync(),
                    JsonSerializerOptions);

                if (parsed.Hello != ExpectedResponse || !parsed.SimpleHellos.Any() || !parsed.SomeRandomNumbers.Any())
                {
                    throw new Exception("Response is not what's expected!");
                }
            }
        }

        [Benchmark]
        public async Task ComplexGrpc()
        {
            for (var i = 0; i < Iterations; ++i)
            {
                var request = new ComplexHelloRequest {Name = "World"};
                request.SimpleHellos.AddRange(
                    Enumerable.Range(0, ComplexCollectionsSize)
                        .Select(i => new HelloRequest {Name = "World " + i}));
                request.SomeRandomNumbers.AddRange(Enumerable.Range(0, ComplexCollectionsSize));

                var result = await _grpcClient.GetMoreComplexHelloAsync(request);

                if (result.Hello != ExpectedResponse || !result.SimpleHellos.Any() || !result.SomeRandomNumbers.Any())
                {
                    throw new Exception("Response is not what's expected!");
                }
            }
        }
    }
}