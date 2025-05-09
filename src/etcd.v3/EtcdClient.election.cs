using Grpc.Core;
using System;
using System.Threading.Tasks;
using System.Threading;
using static V3Electionpb.Election;
using V3Electionpb;

namespace Etcd;

public partial interface IEtcdClient : IDisposable
{
    public ElectionClient ElectionClient { get; }

    CampaignResponse Campaign(CampaignRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<CampaignResponse> CampaignAsync(CampaignRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    ProclaimResponse Proclaim(ProclaimRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<ProclaimResponse> ProclaimAsync(ProclaimRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    LeaderResponse Leader(LeaderRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<LeaderResponse> LeaderAsync(LeaderRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    AsyncServerStreamingCall<LeaderResponse> Observe(LeaderRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);

    ResignResponse Resign(ResignRequest request, Metadata headers = null, DateTime? deadline = null);

    Task<ResignResponse> ResignAsync(ResignRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default);
}

public partial class EtcdClient : IEtcdClient
{
    private ElectionClient electionClient;
    public ElectionClient ElectionClient => electionClient ??= new ElectionClient(callInvoker);

    public CampaignResponse Campaign(CampaignRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return ElectionClient.Campaign(request, headers, deadline);
    }

    public async Task<CampaignResponse> CampaignAsync(CampaignRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return await ElectionClient.CampaignAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public ProclaimResponse Proclaim(ProclaimRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return ElectionClient.Proclaim(request, headers, deadline);
    }

    public async Task<ProclaimResponse> ProclaimAsync(ProclaimRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return await ElectionClient.ProclaimAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public LeaderResponse Leader(LeaderRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return ElectionClient.Leader(request, headers, deadline);
    }

    public async Task<LeaderResponse> LeaderAsync(LeaderRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return await ElectionClient.LeaderAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }

    public AsyncServerStreamingCall<LeaderResponse> Observe(LeaderRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return ElectionClient.Observe(request, headers, deadline, cancellationToken);
    }

    public ResignResponse Resign(ResignRequest request, Metadata headers = null, DateTime? deadline = null)
    {
        return ElectionClient.Resign(request, headers, deadline);
    }

    public async Task<ResignResponse> ResignAsync(ResignRequest request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default)
    {
        return await ElectionClient.ResignAsync(request, headers, deadline, cancellationToken).ConfigureAwait(false);
    }
}