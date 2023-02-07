using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GrpcDemoServer;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;

    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        _logger.LogWarning("Sleeping now");
        await Task.Delay(request.SleepTime);
        return await Task.FromResult(new HelloReply
        {
            Message = $"Hello {request.Name}"
        });
    }

    public override async Task SayHelloStream(HelloRequest request, IServerStreamWriter<StreamHelloReply> responseStream, ServerCallContext context)
    {
        for (int i = 1; i <= 20; i++)
        {
            await Task.Delay(request.SleepTime);
            await responseStream.WriteAsync(new StreamHelloReply
            {
                Message = $"Stream {i}"
            });
        }
    }
}