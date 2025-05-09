using Etcdserverpb;
using Google.Protobuf;
using Grpc.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Etcd;

public partial interface IEtcdClient
{
    Task<EtcdWatcher> WatchAsync(WatchRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    Task<EtcdWatcher> WatchRangeAsync(string path, Metadata headers = null, DateTime? deadline = null, long startRevision = 0, bool noPut = false, bool noDelete = false,
        CancellationToken cancellationToken = default);

    Task<EtcdWatcher> WatchAsync(string key, Metadata headers = null, DateTime? deadline = null, long startRevision = 0, bool noPut = false, bool noDelete = false,
        CancellationToken cancellationToken = default);

    Task WatchRangeBackendAsync(string path, Func<WatchResponse, Task> func, Metadata headers = null, DateTime? deadline = null, long startRevision = 0,
        bool noPut = false, bool noDelete = false, Action<Exception> ex = null, bool reWatchWhenException = false, CancellationToken cancellationToken = default);

    void WatchRangeBackend(string path, Action<WatchResponse> action, Metadata headers = null, DateTime? deadline = null, long startRevision = 0, bool noPut = false,
        bool noDelete = false, Action<Exception> ex = null, bool reWatchWhenException = false);

    Task WatchBackendAsync(string key, Func<WatchResponse, Task> func, Metadata headers = null, DateTime? deadline = null, long startRevision = 0,
        bool noPut = false, bool noDelete = false, Action<Exception> ex = null, bool reWatchWhenException = false, CancellationToken cancellationToken = default);

    void WatchBackend(string key, Action<WatchResponse> action, Metadata headers = null, DateTime? deadline = null, long startRevision = 0, bool noPut = false,
        bool noDelete = false, Action<Exception> ex = null, bool reWatchWhenException = false);
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

    public Task<EtcdWatcher> WatchRangeAsync(string path, Metadata headers = null, DateTime? deadline = null, long startRevision = 0, bool noPut = false, bool noDelete = false,
        CancellationToken cancellationToken = default)
    {
        var req = CreateWatchReq(path, startRevision, noPut, noDelete);
        req.CreateRequest.RangeEnd = ByteString.CopyFromUtf8(GetRangeEnd(path));
        return WatchAsync(req, headers, deadline, cancellationToken);
    }

    public Task<EtcdWatcher> WatchAsync(string key, Metadata headers = null, DateTime? deadline = null, long startRevision = 0, bool noPut = false, bool noDelete = false,
        CancellationToken cancellationToken = default)
    {
        return WatchAsync(CreateWatchReq(key, startRevision, noPut, noDelete), headers, deadline, cancellationToken);
    }

    public async Task WatchRangeBackendAsync(string path, Func<WatchResponse, Task> func, Metadata headers = null, DateTime? deadline = null, long startRevision = 0,
        bool noPut = false, bool noDelete = false, Action<Exception> ex = null, bool reWatchWhenException = false, CancellationToken cancellationToken = default)
    {
        var watcher = await WatchRangeAsync(path, headers, deadline, startRevision, noPut, noDelete, cancellationToken);
        Task.Factory.StartNew(async () =>
        {
            try
            {
                await watcher.ForAllAsync(reWatchWhenException
                    ? i =>
                {
                    startRevision = FindRevision(startRevision, i);
                    return func(i);
                }
                : func, CancellationToken.None);
            }
            catch (Exception e)
            {
                ex?.Invoke(e);
                if (reWatchWhenException)
                {
                    await WatchRangeBackendAsync(path, func, headers, deadline, startRevision, noPut, noDelete, ex, reWatchWhenException, CancellationToken.None);
                }
            }
        });
    }

    public void WatchRangeBackend(string path, Action<WatchResponse> action, Metadata headers = null, DateTime? deadline = null, long startRevision = 0, bool noPut = false,
        bool noDelete = false, Action<Exception> ex = null, bool reWatchWhenException = false)
    {
        var watcher = WatchRangeAsync(path, headers, deadline, startRevision, noPut, noDelete, CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult();
        Task.Factory.StartNew(() =>
        {
            try
            {
                watcher.ForAll(reWatchWhenException
                    ? i =>
                    {
                        startRevision = FindRevision(startRevision, i);
                        action(i);
                    }
                : action);
            }
            catch (Exception e)
            {
                ex?.Invoke(e);
                if (reWatchWhenException)
                {
                    WatchRangeBackend(path, action, headers, deadline, startRevision, noPut, noDelete, ex, reWatchWhenException);
                }
            }
        });
    }

    public async Task WatchBackendAsync(string key, Func<WatchResponse, Task> func, Metadata headers = null, DateTime? deadline = null, long startRevision = 0,
        bool noPut = false, bool noDelete = false, Action<Exception> ex = null, bool reWatchWhenException = false, CancellationToken cancellationToken = default)
    {
        var watcher = await WatchAsync(key, headers, deadline, startRevision, noPut, noDelete, cancellationToken);
        Task.Factory.StartNew(async () =>
        {
            try
            {
                await watcher.ForAllAsync(reWatchWhenException
                    ? i =>
                    {
                        startRevision = FindRevision(startRevision, i);
                        return func(i);
                    }
                : func, CancellationToken.None);
            }
            catch (Exception e)
            {
                ex?.Invoke(e);
                if (reWatchWhenException)
                {
                    await WatchBackendAsync(key, func, headers, deadline, startRevision, noPut, noDelete, ex, reWatchWhenException, CancellationToken.None);
                }
            }
        });
    }

    public void WatchBackend(string key, Action<WatchResponse> action, Metadata headers = null, DateTime? deadline = null, long startRevision = 0, bool noPut = false,
        bool noDelete = false, Action<Exception> ex = null, bool reWatchWhenException = false)
    {
        var watcher = WatchAsync(key, headers, deadline, startRevision, noPut, noDelete, CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult();
        Task.Factory.StartNew(() =>
        {
            try
            {
                watcher.ForAll(reWatchWhenException
                    ? i =>
                    {
                        startRevision = FindRevision(startRevision, i);
                        action(i);
                    }
                : action);
            }
            catch (Exception e)
            {
                ex?.Invoke(e);
                if (reWatchWhenException)
                {
                    WatchBackend(key, action, headers, deadline, startRevision, noPut, noDelete, ex, reWatchWhenException);
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

    private static long FindRevision(long startRevision, WatchResponse i)
    {
        if (i.CompactRevision >= startRevision)
        {
            startRevision = i.CompactRevision + 1;
        }

        if (i.Header.Revision >= startRevision)
        {
            startRevision = i.Header.Revision + 1;
        }

        return startRevision;
    }
}