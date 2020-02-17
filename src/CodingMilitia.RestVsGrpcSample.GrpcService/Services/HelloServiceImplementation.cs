using System.Linq;
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

        public override Task<ComplexHelloResponse> GetMoreComplexHello(ComplexHelloRequest request, ServerCallContext context)
        {
            var hello = $"Hello {request.Name}!";
            var response = new ComplexHelloResponse {Hello = hello};
            response.SimpleHellos.AddRange(request.SimpleHellos.Select(r => new HelloResponse{ Hello = $"Hello {r.Name}!"}));
            response.SomeRandomNumbers.AddRange(request.SomeRandomNumbers);
            return Task.FromResult(response);
        }
    }
}
