using Grpc.Core;
using GrpcDemoServer;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GrpcClient
{
    public class GreeterClient
    {
        private readonly Greeter.GreeterClient _client;

        public GreeterClient()
        {
            var channel = CreateChannel();
            _client = new Greeter.GreeterClient(channel);
        }

        public async Task Execute()
        {
            using (var transfer = _client.SayHelloStream(GetRequest("Test", 5)))
            {
                await ExecuteStream(transfer);
            }
        }

        private static Channel CreateChannel()
        {
            var channel = new Channel("<SERVERIP>", 1234, ChannelCredentials.Insecure, new[]
            {
                new ChannelOption( "grpc.keepalive_time_ms", 1000 )
            });

            return channel;
        }

        private async Task ExecuteStream(AsyncServerStreamingCall<StreamHelloReply> transfer)
        {
            try
            {
                while (await transfer.ResponseStream.MoveNext(CancellationToken.None).ConfigureAwait(false))
                {
                    var current = transfer.ResponseStream.Current;
                    _client.SayHello(GetRequest(current.Message, 1));
                    Console.WriteLine(current.Message);
                }
            }
            catch (RpcException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private HelloRequest GetRequest(string name, int sleepTime)
        {
            return new HelloRequest()
            {
                Name = name,
                SleepTime = sleepTime == 0 ? 1 : sleepTime * 1000
            };
        }
    }
}
