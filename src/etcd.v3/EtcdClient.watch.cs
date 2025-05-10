using Etcdserverpb;
using Google.Protobuf;
using Grpc.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Etcd;

public partial interface IEtcdClient
{
    public Watch.WatchClient WatchClient { get; }

    Task<EtcdWatcher> WatchAsync(WatchRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    Task<EtcdWatcher> WatchRangeAsync(string path, Metadata headers = null, DateTime? deadline = null, long startRevision = 0, bool noPut = false, bool noDelete = false,
        CancellationToken cancellationToken = default);

    Task<EtcdWatcher> WatchAsync(string key, Metadata headers = null, DateTime? deadline = null, long startRevision = 0, bool noPut = false, bool noDelete = false,
        CancellationToken cancellationToken = default);

    Task WatchRangeBackendAsync(string path, Func<WatchResponse, Task> func, Metadata headers = null, DateTime? deadline = null, long startRevision = 0,
        bool noPut = false, bool noDelete = false, Action<Exception> ex = null, bool reWatchWhenException = false, CancellationToken cancellationToken = default);

    Task WatchBackendAsync(string key, Func<WatchResponse, Task> func, Metadata headers = null, DateTime? deadline = null, long startRevision = 0,
        bool noPut = false, bool noDelete = false, Action<Exception> ex = null, bool reWatchWhenException = false, CancellationToken cancellationToken = default);
}

public partial class EtcdClient : IEtcdClient
{
    private Watch.WatchClient watchClient;
    public Watch.WatchClient WatchClient => watchClient ??= new Watch.WatchClient(callInvoker);

    public async Task<EtcdWatcher> WatchAsync(WatchRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        var stream = WatchClient.Watch(headers, deadline, cancellationToken);
        await stream.RequestStream.WriteAsync(new WatchRequest() { CreateRequest = request.CreateRequest }, cancellationToken).ConfigureAwait(false);
        return new EtcdWatcher(stream);
    }

    public Task<EtcdWatcher> WatchRangeAsync(string path, Metadata headers = null, DateTime? deadline = null, long startRevision = 0, bool noPut = false, bool noDelete = false,
        CancellationToken cancellationToken = default)
    {
        var req = CreateWatchReq(path, startRevision, noPut, noDelete);
        req.CreateRequest.RangeEnd = ByteString.CopyFromUtf8(path.GetRangeEnd());
        return WatchAsync(req, headers, deadline, cancellationToken);
    }

    public Task<EtcdWatcher> WatchAsync(string key, Metadata headers = null, DateTime? deadline = null, long startRevision = 0, bool noPut = false, bool noDelete = false,
        CancellationToken cancellationToken = default)
    {
        return WatchAsync(CreateWatchReq(key, startRevision, noPut, noDelete), headers, deadline, cancellationToken);
    }

    public Task WatchRangeBackendAsync(string path, Func<WatchResponse, Task> func, Metadata headers = null, DateTime? deadline = null, long startRevision = 0,
        bool noPut = false, bool noDelete = false, Action<Exception> ex = null, bool reWatchWhenException = false, CancellationToken cancellationToken = default)
    {
        return Task.Factory.StartNew(async () =>
        {
            try
            {
                var watcher = await WatchRangeAsync(path, headers, deadline, startRevision, noPut, noDelete, cancellationToken);
                await watcher.ForAllAsync(reWatchWhenException
                    ? i =>
                {
                    startRevision = i.FindRevision(startRevision);
                    return func(i);
                }
                : func, CancellationToken.None);
            }
            catch (Exception e)
            {
                ex?.Invoke(e);
                if (reWatchWhenException)
                {
                    WatchRangeBackendAsync(path, func, headers, deadline, startRevision, noPut, noDelete, ex, reWatchWhenException, CancellationToken.None);
                }
            }
        });
    }

    public Task WatchBackendAsync(string key, Func<WatchResponse, Task> func, Metadata headers = null, DateTime? deadline = null, long startRevision = 0,
        bool noPut = false, bool noDelete = false, Action<Exception> ex = null, bool reWatchWhenException = false, CancellationToken cancellationToken = default)
    {
        return Task.Factory.StartNew(async () =>
        {
            var watcher = await WatchAsync(key, headers, deadline, startRevision, noPut, noDelete, cancellationToken);
            try
            {
                await watcher.ForAllAsync(reWatchWhenException
                    ? i =>
                    {
                        startRevision = i.FindRevision(startRevision);
                        return func(i);
                    }
                : func, CancellationToken.None);
            }
            catch (Exception e)
            {
                ex?.Invoke(e);
                if (reWatchWhenException)
                {
                    WatchBackendAsync(key, func, headers, deadline, startRevision, noPut, noDelete, ex, reWatchWhenException, CancellationToken.None);
                }
            }
        });
    }

    private static WatchRequest CreateWatchReq(string key, long startRevision, bool noPut, bool noDelete)
    {
        var req = new WatchCreateRequest
        {
            Key = ByteString.CopyFromUtf8(key),
            StartRevision = startRevision,
            ProgressNotify = true,
            PrevKv = true,
        };
        if (noPut)
        {
            req.Filters.Add(WatchCreateRequest.Types.FilterType.Noput);
        }
        if (noDelete)
        {
            req.Filters.Add(WatchCreateRequest.Types.FilterType.Nodelete);
        }

        return new WatchRequest()
        {
            CreateRequest = req
        };
    }
}