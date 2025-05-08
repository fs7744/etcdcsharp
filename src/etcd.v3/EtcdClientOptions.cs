using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using System;

namespace Etcd;

public class EtcdClientOptions
{
    public string[] Address { get; set; }

    public Action<GrpcChannelOptions>? GrpcChannelOptions { get; set; }
    public Interceptor[] Interceptors { get; set; }
}
