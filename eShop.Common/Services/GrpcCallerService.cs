using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;

namespace eShop.Common.Services
{
    public static class GrpcCallerService
    {
        public static async Task<TResponse> CallService<TResponse>(string urlGrpc, ILogger logger, Func<GrpcChannel, Task<TResponse>> func)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);

            var channel = GrpcChannel.ForAddress(urlGrpc);

            if (logger != null)
            {
                logger.LogInformation("Creating grpc client base address urlGrpc ={@urlGrpc}, BaseAddress={@BaseAddress} ", urlGrpc, channel.Target);
            }
            try
            {
                return await func(channel);
            }
            catch (RpcException e)
            {
                if (logger != null)
                {
                    logger.LogError("Error calling via grpc: {Status} - {Message}", e.Status, e.Message);
                }
                return default;
            }
            finally
            {
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", false);
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", false);
            }


        }

        public static async Task CallService(string urlGrpc, ILogger logger, Func<GrpcChannel, Task> func)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);

            var channel = GrpcChannel.ForAddress(urlGrpc);
            if (logger != null)
            {
                logger.LogDebug("Creating grpc client base address {@httpClient.BaseAddress} ", channel.Target);
            }
            try
            {
                await func(channel);
            }
            catch (RpcException e)
            {
                if (logger != null)
                {
                    logger.LogError("Error calling via grpc: {Status} - {Message}", e.Status, e.Message);
                }
            }
            finally
            {
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", false);
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", false);
            }
        }
    }
}
