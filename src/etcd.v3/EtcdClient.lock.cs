using Google.Protobuf;
using Grpc.Core;
using System;
using System.Threading;
using System.Threading.Tasks;
using V3Lockpb;
using static V3Lockpb.Lock;

namespace Etcd;

public partial interface IEtcdClient : IDisposable
{
    public LockClient LockClient { get; }

    LockResponse Lock(LockRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<LockResponse> LockAsync(LockRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    LockResponse Lock(string name, Metadata headers = null, DateTime? deadline = null);

    Task<LockResponse> LockAsync(string name, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    UnlockResponse Unlock(UnlockRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<UnlockResponse> UnlockAsync(UnlockRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    UnlockResponse Unlock(string name, Metadata headers = null, DateTime? deadline = null);

    Task<UnlockResponse> UnlockAsync(string name, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);
}

public partial class EtcdClient : IEtcdClient
{
    private LockClient lockClient;
    public LockClient LockClient => lockClient ??= new LockClient(callInvoker);

    public LockResponse Lock(LockRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return LockClient.Lock(request, headers, deadline);
    }

    public async Task<LockResponse> LockAsync(LockRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return await LockClient.LockAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public LockResponse Lock(string name, Metadata headers = null, DateTime? deadline = null)
    {
        return Lock(new LockRequest() { Name = ByteString.CopyFromUtf8(name) }, headers, deadline);
    }

    public async Task<LockResponse> LockAsync(string name, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return await LockAsync(new LockRequest() { Name = ByteString.CopyFromUtf8(name) }, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public UnlockResponse Unlock(UnlockRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return LockClient.Unlock(request, headers, deadline);
    }

    public async Task<UnlockResponse> UnlockAsync(UnlockRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return await LockClient.UnlockAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public UnlockResponse Unlock(string name, Metadata headers = null, DateTime? deadline = null)
    {
        return Unlock(new UnlockRequest() { Key = ByteString.CopyFromUtf8(name) }, headers, deadline);
    }

    public async Task<UnlockResponse> UnlockAsync(string name, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return await UnlockAsync(new UnlockRequest() { Key = ByteString.CopyFromUtf8(name) }, headers, deadline, cancellationToken).ConfigureAwait(false);
    }
}