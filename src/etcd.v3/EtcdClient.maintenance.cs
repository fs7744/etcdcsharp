using Etcdserverpb;
using Grpc.Core;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Etcdserverpb.Maintenance;

namespace Etcd;

public partial interface IEtcdClient : IDisposable
{
    public MaintenanceClient MaintenanceClient { get; }

    AlarmResponse Alarm(AlarmRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<AlarmResponse> AlarmAsync(AlarmRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    StatusResponse Status(StatusRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<StatusResponse> StatusAsync(StatusRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    DefragmentResponse Defragment(DefragmentRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<DefragmentResponse> DefragmentAsync(DefragmentRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    HashResponse Hash(HashRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<HashResponse> HashAsync(HashRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    HashKVResponse HashKV(HashKVRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<HashKVResponse> HashKVAsync(HashKVRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    AsyncServerStreamingCall<SnapshotResponse> Snapshot(SnapshotRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    MoveLeaderResponse MoveLeader(MoveLeaderRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<MoveLeaderResponse> MoveLeaderAsync(MoveLeaderRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);
}

public partial class EtcdClient : IEtcdClient
{
    private MaintenanceClient maintenanceClient;
    public MaintenanceClient MaintenanceClient => maintenanceClient ??= new MaintenanceClient(callInvoker);

    public AlarmResponse Alarm(AlarmRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return MaintenanceClient.Alarm(request, headers, deadline);
    }

    public async Task<AlarmResponse> AlarmAsync(AlarmRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return await MaintenanceClient.AlarmAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public StatusResponse Status(StatusRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return MaintenanceClient.Status(request, headers, deadline);
    }

    public async Task<StatusResponse> StatusAsync(StatusRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return await MaintenanceClient.StatusAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public DefragmentResponse Defragment(DefragmentRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return MaintenanceClient.Defragment(request, headers, deadline);
    }

    public async Task<DefragmentResponse> DefragmentAsync(DefragmentRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return await MaintenanceClient.DefragmentAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public HashResponse Hash(HashRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return MaintenanceClient.Hash(request, headers, deadline);
    }

    public async Task<HashResponse> HashAsync(HashRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return await MaintenanceClient.HashAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public HashKVResponse HashKV(HashKVRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return MaintenanceClient.HashKV(request, headers, deadline);
    }

    public async Task<HashKVResponse> HashKVAsync(HashKVRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return await MaintenanceClient.HashKVAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public AsyncServerStreamingCall<SnapshotResponse> Snapshot(SnapshotRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return MaintenanceClient.Snapshot(request, headers, deadline, cancellationToken);
    }

    public MoveLeaderResponse MoveLeader(MoveLeaderRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return MaintenanceClient.MoveLeader(request, headers, deadline);
    }

    public async Task<MoveLeaderResponse> MoveLeaderAsync(MoveLeaderRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return await MaintenanceClient.MoveLeaderAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }
}