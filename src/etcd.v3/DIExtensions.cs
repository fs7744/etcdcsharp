using Google.Protobuf;
using Grpc.Net.Client.Balancer;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Etcd;

public static class DIExtensions
{
    public static IServiceCollection AddEtcdClient(this IServiceCollection services)
    {
        services.AddSingleton<IEtcdClientFactory, EtcdClientFactory>();
        services.AddSingleton<StaticAddressResolverFactory>();
        services.AddSingleton<ResolverFactory>(i => i.GetRequiredService<StaticAddressResolverFactory>().resolverFactory);
        return services;
    }

    public static string ToStrUtf8(this ByteString bytes)
    {
        return Encoding.UTF8.GetString(bytes.Span);
    }
}