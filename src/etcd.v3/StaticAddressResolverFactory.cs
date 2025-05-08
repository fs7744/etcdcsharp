using Grpc.Net.Client;
using Grpc.Net.Client.Balancer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Etcd;

public class StaticAddressResolverFactory
{
    internal readonly ConcurrentDictionary<string, IEnumerable<BalancerAddress>> address = new ConcurrentDictionary<string, IEnumerable<BalancerAddress>>();
    internal readonly StaticResolverFactory resolverFactory;

    public StaticAddressResolverFactory()
    {
        resolverFactory = new StaticResolverFactory(FindAddress);
    }

    internal string AddAddress(string[] addresss, GrpcChannelOptions grpcChannelOptions)
    {
        var key = $"static://{Guid.NewGuid().ToString()}";
        address[key] = addresss.Select(a =>
        {
            var i = new Uri(a);
            return new BalancerAddress(i.Host, i.Port);
        }).ToArray();
        return key;
    }

    private IEnumerable<BalancerAddress> FindAddress(Uri uri)
    {
        return address[uri.ToString()];
    }
}