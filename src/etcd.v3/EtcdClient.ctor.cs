using Grpc.Core;
using Grpc.Net.Client;
using System;

namespace Etcd;

public partial interface IEtcdClient : IDisposable
{
}

public partial class EtcdClient : IEtcdClient
{
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
}