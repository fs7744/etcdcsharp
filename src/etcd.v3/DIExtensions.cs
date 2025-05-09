using Google.Protobuf;
using Grpc.Net.Client.Balancer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;

namespace Etcd;

public static class DIExtensions
{
    public static IServiceCollection UseEtcdClient(this IServiceCollection services, TimeSpan? dnsRefreshInterval = null)
    {
        services.AddSingleton<IEtcdClientFactory, EtcdClientFactory>();
        services.AddSingleton<ResolverFactory>(new DnsResolverFactory(dnsRefreshInterval.GetValueOrDefault(TimeSpan.FromSeconds(30))));
        services.AddSingleton<StaticAddressResolverFactory>();
        services.AddSingleton<ResolverFactory>(i => i.GetRequiredService<StaticAddressResolverFactory>().resolverFactory);
        return services;
    }

    public static IServiceCollection AddEtcdClient(this IServiceCollection services, string key, EtcdClientOptions options)
    {
        services.AddKeyedSingleton<IEtcdClient>(key, (i, k) => i.GetRequiredService<IEtcdClientFactory>().CreateClient(options));
        return services;
    }

    public static string ToStrUtf8(this ByteString bytes)
    {
        return Encoding.UTF8.GetString(bytes.Span);
    }
}