using Etcdserverpb;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Etcd;

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

    public async Task ForAllAsync(Func<WatchResponse, Task> func, CancellationToken cancellationToken = default)
    {
        try
        {
            var s = stream.ResponseStream;
            while (await s.MoveNext(cancellationToken).ConfigureAwait(continueOnCapturedContext: false))
            {
                await func(s.Current);
            }
        }
        finally
        {
            await CancelAsync(cancellationToken);
        }
    }
}