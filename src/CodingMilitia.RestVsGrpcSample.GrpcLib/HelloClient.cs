using System.Threading.Tasks;
using CodingMilitia.RestVsGrpcSample.GrpcLib.Generated;
using Grpc.Core;
using static CodingMilitia.RestVsGrpcSample.GrpcLib.Generated.HelloService;

namespace CodingMilitia.RestVsGrpcSample.GrpcLib
{
    public class HelloClient
    {
        private Channel _channel;
        private HelloServiceClient _client;
        
        public void Connect(){
            _channel = new Channel("localhost:1234", ChannelCredentials.Insecure);
            _client = new HelloServiceClient(_channel);
        }

        public async Task<string> SendMessageAsync(){
            return (await _client.GetHelloAsync(new HelloRequest())).Hello;
        }

        public async Task ShutdownAsync(){
            await _channel.ShutdownAsync();
        }
    }
}