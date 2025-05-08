using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using Grpc.Net.Client.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Etcd;

public partial interface IEtcdClientFactory
{
    IEtcdClient CreateClient(EtcdClientOptions options);
}

public partial class EtcdClientFactory : IEtcdClientFactory
{
    private static readonly MethodConfig defaultGrpcMethodConfig = new()
    {
        Names = { MethodName.Default },
        RetryPolicy = new RetryPolicy
        {
            MaxAttempts = 5,
            InitialBackoff = TimeSpan.FromSeconds(1),
            MaxBackoff = TimeSpan.FromSeconds(5),
            BackoffMultiplier = 1.5,
            RetryableStatusCodes = { StatusCode.Unavailable }
        }
    };

    private static readonly RetryThrottlingPolicy defaultRetryThrottlingPolicy = new()
    {
        MaxTokens = 10,
        TokenRatio = 0.1
    };

    private readonly IServiceProvider serviceProvider;
    private readonly StaticAddressResolverFactory resolverFactory;

    public EtcdClientFactory(IServiceProvider serviceProvider, StaticAddressResolverFactory resolverFactory)
    {
        this.serviceProvider = serviceProvider;
        this.resolverFactory = resolverFactory;
    }

    public static IEtcdClientFactory Create()
    {
        ServiceCollection services = new();
        services.AddEtcdClient();
        var p = services.BuildServiceProvider();
        return p.GetRequiredService<IEtcdClientFactory>();
    }

    public IEtcdClient CreateClient(EtcdClientOptions options)
    {
        var grpcChannelOptions = new GrpcChannelOptions()
        {
            ServiceConfig = new ServiceConfig
            {
                MethodConfigs = { defaultGrpcMethodConfig },
                RetryThrottling = defaultRetryThrottlingPolicy,
                LoadBalancingConfigs = { new RoundRobinConfig() }
            },
            HttpHandler = new SocketsHttpHandler()
            {
                KeepAlivePingDelay = TimeSpan.FromSeconds(30),
                KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
                KeepAlivePingPolicy = HttpKeepAlivePingPolicy.Always
            },
            DisposeHttpClient = true,
            ThrowOperationCanceledOnCancellation = true,
            Credentials = ChannelCredentials.Insecure,
            ServiceProvider = serviceProvider
        };
        options.GrpcChannelOptions?.Invoke(grpcChannelOptions);

        GrpcChannel channel;
        if (options.Address.Length == 1)
            channel = GrpcChannel.ForAddress(options.Address[0], grpcChannelOptions);
        else
        {
            channel = GrpcChannel.ForAddress(resolverFactory.AddAddress(options.Address, grpcChannelOptions));
        }
        CallInvoker callInvoker = options.Interceptors != null && options.Interceptors.Length > 0
            ? channel.Intercept(options.Interceptors)
            : channel.CreateCallInvoker();

        return new EtcdClient(channel, callInvoker);
    }
}