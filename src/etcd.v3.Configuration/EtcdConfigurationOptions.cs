using System;

namespace Etcd.Configuration;

public class EtcdConfigurationOptions
{
    public string Prefix { get; init; }

    public bool RemovePrefix { get; init; }

    public TimeSpan? DnsRefreshInterval { get; init; }

    public EtcdClientOptions? EtcdClientOptions { get; init; }
}