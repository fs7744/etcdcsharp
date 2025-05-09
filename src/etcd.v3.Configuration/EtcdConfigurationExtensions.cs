using Etcd.Configuration;

namespace Microsoft.Extensions.Configuration;

public static class EtcdConfigurationExtensions
{
    public static IConfigurationBuilder UseEtcd(this IConfigurationBuilder builder, EtcdConfigurationOptions options)
    {
        builder.Add(new EtcdConfigurationSource(options));
        return builder;
    }
}