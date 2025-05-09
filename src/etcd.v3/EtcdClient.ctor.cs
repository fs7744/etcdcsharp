using Grpc.Core;
using Grpc.Net.Client;
using System;

namespace Etcd;

public partial interface IEtcdClient : IDisposable
{
}

public partial class EtcdClient : IEtcdClient
{
    private const string rangeEndString = "\x00";
    private GrpcChannel channel;
    private CallInvoker callInvoker;

    public EtcdClient(GrpcChannel channel, CallInvoker callInvoker)
    {
        this.channel = channel;
        this.callInvoker = callInvoker;
    }

    public void Dispose()
    {
        channel?.Dispose();
    }

    public static string GetRangeEnd(string prefixKey)
    {
        if (prefixKey.Length == 0)
        {
            return rangeEndString;
        }

        var s = prefixKey.AsSpan();
        char cc = s[prefixKey.Length - 1];
        ++cc;
        Span<char> c = stackalloc char[1] { cc };
        return string.Concat(s.Slice(0, prefixKey.Length - 1), c);
    }
}