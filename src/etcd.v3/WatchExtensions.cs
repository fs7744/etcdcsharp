using Etcdserverpb;
using Grpc.Core;

namespace ETCD.V3
{
    public static class WatchExtensions
    {
        /// <summary>
        /// Watch watches for events happening or that have happened. Both input and output
        /// are streams; the input stream is for creating and canceling watchers and the output
        /// stream sends events. One watch RPC can watch on multiple key ranges, streaming events
        /// for several watches at once. The entire event history can be watched starting from the
        /// last compaction revision.
        /// </summary>
        /// <returns>The call object.</returns>
        public static AsyncDuplexStreamingCall<WatchRequest, WatchResponse> Watch(this Client client)
        {
            return client.Watch.Watch();
        }
    }
}