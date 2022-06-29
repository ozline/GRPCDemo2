using CXMP.Protos;
using Grpc.Core;
namespace CXBIM.UserService.API.GrpcService
{
    public class GreeterService : Greeter.GreeterClient
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }


        public Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }

    //private readonly ICX_UserService _UserService;
    //grpc服务客户端
}

