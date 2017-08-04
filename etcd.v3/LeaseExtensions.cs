using Etcdserverpb;
using Grpc.Core;

namespace ETCD.V3
{
    public static class LeaseExtensions
    {
        #region LeaseGrant

        public static LeaseGrantRequest CreateLeaseGrantRequest(this Client client, long ttl, long id)
        {
            return new LeaseGrantRequest()
            {
                TTL = ttl,
                ID = id
            };
        }

        public static LeaseGrantResponse LeaseGrant(this Client client, long ttl, long id)
        {
            var request = client.CreateLeaseGrantRequest(ttl, id);
            return client.Lease.LeaseGrant(request, client.CallToken);
        }

        public static AsyncUnaryCall<LeaseGrantResponse> LeaseGrantAsync(this Client client, long ttl, long id)
        {
            var request = client.CreateLeaseGrantRequest(ttl, id);
            return client.Lease.LeaseGrantAsync(request, client.CallToken);
        }

        #endregion LeaseGrant

        #region LeaseRevoke

        public static LeaseRevokeRequest CreateLeaseRevokeRequest(this Client client, long id)
        {
            return new LeaseRevokeRequest()
            {
                ID = id
            };
        }

        public static LeaseRevokeResponse LeaseRevoke(this Client client, long id)
        {
            var request = client.CreateLeaseRevokeRequest(id);
            return client.Lease.LeaseRevoke(request, client.CallToken);
        }

        public static AsyncUnaryCall<LeaseRevokeResponse> LeaseRevokeAsync(this Client client, long id)
        {
            var request = client.CreateLeaseRevokeRequest(id);
            return client.Lease.LeaseRevokeAsync(request, client.CallToken);
        }

        #endregion LeaseRevoke

        public static AsyncDuplexStreamingCall<LeaseKeepAliveRequest, LeaseKeepAliveResponse> LeaseKeepAlive(this Client client)
        {
            return client.Lease.LeaseKeepAlive(client.CallToken);
        }

        #region LeaseTimeToLive

        public static LeaseTimeToLiveRequest CreateLeaseTimeToLiveRequest(this Client client, long id, bool keys = false)
        {
            return new LeaseTimeToLiveRequest()
            {
                ID = id,
                Keys = keys
            };
        }

        public static LeaseTimeToLiveResponse LeaseTimeToLive(this Client client, long id, bool keys = false)
        {
            var request = client.CreateLeaseTimeToLiveRequest(id, keys);
            return client.Lease.LeaseTimeToLive(request, client.CallToken);
        }

        public static AsyncUnaryCall<LeaseTimeToLiveResponse> LeaseTimeToLiveAsync(this Client client, long id, bool keys = false)
        {
            var request = client.CreateLeaseTimeToLiveRequest(id, keys);
            return client.Lease.LeaseTimeToLiveAsync(request, client.CallToken);
        }

        #endregion LeaseTimeToLive
    }
}