using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Etcd.Configuration;

internal class EtcdConfigurationSource : ConfigurationProvider, IConfigurationSource, IDisposable
{
    private readonly string prefix;
    private readonly bool removePrefix;
    private readonly IEtcdClient client;

    public EtcdConfigurationSource(EtcdConfigurationOptions options)
    {
        this.prefix = options.Prefix;
        removePrefix = options.RemovePrefix && !string.IsNullOrEmpty(prefix);
        this.client = EtcdClientFactory.Create(options.DnsRefreshInterval).CreateClient(options.EtcdClientOptions);
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return this;
    }

    public void Dispose()
    {
        client.Dispose();
    }

    public override void Load()
    {
        var d = client.GetRangeValueUtf8(prefix);
        foreach (var item in d)
        {
            Data.Add(removePrefix ? item.Key.Substring(prefix.Length) : item.Key, item.Value);
        }
        client.WatchRangeBackendAsync(prefix, i =>
        {
            if (i.Events.Count > 0)
            {
                foreach (var j in i.Events)
                {
                    var k = j.Kv.Key.ToStrUtf8();
                    k = removePrefix ? k.Substring(prefix.Length) : k;
                    switch (j.Type)
                    {
                        case Mvccpb.Event.Types.EventType.Put:

                            base.Set(k, j.Kv.Value.ToStrUtf8());
                            break;

                        case Mvccpb.Event.Types.EventType.Delete:
                            Data.Remove(k);
                            break;

                        default:
                            break;
                    }
                }
                OnReload();
            }
            return Task.CompletedTask;
        }, reWatchWhenException: true);
    }

    public override void Set(string key, string value)
    {
        base.Set(key, value);
        client.Put(removePrefix ? $"{removePrefix}{key}" : key, value);
    }
}