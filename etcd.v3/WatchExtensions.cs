using Etcdserverpb;
using Grpc.Core;

namespace ETCD.V3
{
    public static class WatchExtensions
    {
        public static AsyncDuplexStreamingCall<WatchRequest, WatchResponse> Watch(this Client client)
        {
            return client.Watch.Watch(client.CallToken);
        }
    }
}