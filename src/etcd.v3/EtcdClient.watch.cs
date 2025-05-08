using Etcdserverpb;
using Google.Protobuf;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Etcd;

public partial interface IEtcdClient
{
    Task<EtcdWatcher> WatchAsync(WatchRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    Task<EtcdWatcher> WatchAsync(string path, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);
}

public partial class EtcdClient : IEtcdClient
{
    public async Task<EtcdWatcher> WatchAsync(WatchRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        var stream = WatchClient.Watch(headers, deadline, cancellationToken);
        await stream.RequestStream.WriteAsync(new WatchRequest() { CreateRequest = request.CreateRequest }, cancellationToken);
        return new EtcdWatcher(stream);
    }

    public Task<EtcdWatcher> WatchAsync(string path, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default) => WatchAsync(new WatchRequest()
        {
            CreateRequest = new WatchCreateRequest
            {
                Key = ByteString.CopyFromUtf8(path),
                RangeEnd = ByteString.CopyFromUtf8(GetRangeEnd(path)),
                ProgressNotify = true,
                PrevKv = true
            }
        }, headers, deadline, cancellationToken);
}

public class EtcdWatcher : IDisposable
{
    private readonly AsyncDuplexStreamingCall<WatchRequest, WatchResponse> stream;

    public EtcdWatcher(AsyncDuplexStreamingCall<WatchRequest, WatchResponse> stream)
    {
        this.stream = stream;
    }

    public IAsyncEnumerable<WatchResponse> ReadAllAsync(CancellationToken cancellationToken = default)
    {
        return stream.ResponseStream.ReadAllAsync(cancellationToken);
    }

    public async Task CancelAsync(CancellationToken cancellationToken = default)
    {
        WatchRequest request = new() { CancelRequest = new WatchCancelRequest { } };
        await stream.RequestStream.WriteAsync(request, cancellationToken);
        stream.Dispose();
    }

    public void Dispose()
    {
        stream.Dispose();
    }
}