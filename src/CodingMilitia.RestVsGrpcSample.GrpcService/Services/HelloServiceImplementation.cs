using System.Threading.Tasks;
using CodingMilitia.RestVsGrpcSample.GrpcService.Generated;
using Grpc.Core;

namespace CodingMilitia.RestVsGrpcSample.GrpcService
{
    public class HelloServiceImplementation : HelloService.HelloServiceBase
    {
        public override Task<HelloResponse> GetHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloResponse{ Hello = $"Hello {request.Name}!"});
        }
    }
}
