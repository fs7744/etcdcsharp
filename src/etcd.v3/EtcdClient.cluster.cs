using Etcdserverpb;
using Grpc.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Etcd;

public partial interface IEtcdClient : IDisposable
{
    public Cluster.ClusterClient ClusterClient { get; }

    MemberAddResponse MemberAdd(MemberAddRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<MemberAddResponse> MemberAddAsync(MemberAddRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    MemberRemoveResponse MemberRemove(MemberRemoveRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<MemberRemoveResponse> MemberRemoveAsync(MemberRemoveRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    MemberUpdateResponse MemberUpdate(MemberUpdateRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<MemberUpdateResponse> MemberUpdateAsync(MemberUpdateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    MemberListResponse MemberList(MemberListRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<MemberListResponse> MemberListAsync(MemberListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
}

public partial class EtcdClient : IEtcdClient
{
    private Cluster.ClusterClient clusterClient;
    public Cluster.ClusterClient ClusterClient => clusterClient ??= new Cluster.ClusterClient(callInvoker);

    public MemberAddResponse MemberAdd(MemberAddRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return ClusterClient.MemberAdd(request, headers, deadline);
    }

    public async Task<MemberAddResponse> MemberAddAsync(MemberAddRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
    {
        return await ClusterClient.MemberAddAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public MemberRemoveResponse MemberRemove(MemberRemoveRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return ClusterClient.MemberRemove(request, headers, deadline);
    }

    public async Task<MemberRemoveResponse> MemberRemoveAsync(MemberRemoveRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
    {
        return await ClusterClient.MemberRemoveAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public MemberUpdateResponse MemberUpdate(MemberUpdateRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return ClusterClient.MemberUpdate(request, headers, deadline);
    }

    public async Task<MemberUpdateResponse> MemberUpdateAsync(MemberUpdateRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
    {
        return await ClusterClient.MemberUpdateAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public MemberListResponse MemberList(MemberListRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return ClusterClient.MemberList(request, headers, deadline);
    }

    public async Task<MemberListResponse> MemberListAsync(MemberListRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
    {
        return await ClusterClient.MemberListAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }
}