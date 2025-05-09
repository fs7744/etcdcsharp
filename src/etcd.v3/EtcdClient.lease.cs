using Etcdserverpb;
using Grpc.Core;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Etcdserverpb.Lease;

namespace Etcd;

public partial interface IEtcdClient : IDisposable
{
    public LeaseClient LeaseClient { get; }

    LeaseGrantResponse LeaseGrant(LeaseGrantRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<LeaseGrantResponse> LeaseGrantAsync(LeaseGrantRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    LeaseRevokeResponse LeaseRevoke(LeaseRevokeRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<LeaseRevokeResponse> LeaseRevokeAsync(LeaseRevokeRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    LeaseTimeToLiveResponse LeaseTimeToLive(LeaseTimeToLiveRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<LeaseTimeToLiveResponse> LeaseTimeToLiveAsync(LeaseTimeToLiveRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    AsyncDuplexStreamingCall<LeaseKeepAliveRequest, LeaseKeepAliveResponse> LeaseKeepAlive(Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
}

public partial class EtcdClient : IEtcdClient
{
    private LeaseClient leaseClient;
    public LeaseClient LeaseClient => leaseClient ??= new LeaseClient(callInvoker);

    public LeaseGrantResponse LeaseGrant(LeaseGrantRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return LeaseClient.LeaseGrant(request, headers, deadline);
    }

    public async Task<LeaseGrantResponse> LeaseGrantAsync(LeaseGrantRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return await LeaseClient.LeaseGrantAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public LeaseRevokeResponse LeaseRevoke(LeaseRevokeRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return LeaseClient.LeaseRevoke(request, headers, deadline);
    }

    public async Task<LeaseRevokeResponse> LeaseRevokeAsync(LeaseRevokeRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return await LeaseClient.LeaseRevokeAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public LeaseTimeToLiveResponse LeaseTimeToLive(LeaseTimeToLiveRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return LeaseClient.LeaseTimeToLive(request, headers, deadline);
    }

    public async Task<LeaseTimeToLiveResponse> LeaseTimeToLiveAsync(LeaseTimeToLiveRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return await LeaseClient.LeaseTimeToLiveAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public AsyncDuplexStreamingCall<LeaseKeepAliveRequest, LeaseKeepAliveResponse> LeaseKeepAlive(Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
    {
        return LeaseClient.LeaseKeepAlive(headers, deadline, cancellationToken);
    }
}