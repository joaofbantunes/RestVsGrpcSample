using System.Threading.Tasks;
using Grpc.Core;
using CodingMilitia.RestVsGrpcSample.GrpcLib.Generated;
using static CodingMilitia.RestVsGrpcSample.GrpcLib.Generated.HelloService;

namespace CodingMilitia.RestVsGrpcSample.GrpcLib
{
    public class HelloServiceImplementation : HelloServiceBase {
        public override Task<Generated.HelloResponse> GetHello(Generated.HelloRequest request, Grpc.Core.ServerCallContext context)
        {
            return Task.FromResult(new Generated.HelloResponse{ Hello = "Hello World!" });
        }
    }

    public class HelloServer
    {
       private Server _server;

       public HelloServer()
       {
           _server = new Server 
           {
               Services = { HelloService.BindService(new HelloServiceImplementation()) },
               Ports = { new ServerPort("localhost", 1234, ServerCredentials.Insecure) }
           };
       }

        public void Listen()
        {
            _server.Start();
        }

        public async Task ShutdownAsync()
        {
            await _server.ShutdownAsync();
        }
    }
}